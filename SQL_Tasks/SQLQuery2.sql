--1. Customer Spending Analysis#
--Write a query that uses variables to find the total amount spent by customer ID 1. Display a message showing whether they are a VIP customer (spent > $5000) or regular customer.
DECLARE @CustomerID INT = 1;
DECLARE @TotalSpent DECIMAL(10,2);

SELECT @TotalSpent = SUM(oi.quantity * oi.list_price * (1 - oi.discount))
FROM sales.orders o
JOIN sales.order_items oi ON o.order_id = oi.order_id
WHERE o.customer_id = @CustomerID;

SELECT 
    @CustomerID AS customer_id,
    @TotalSpent AS total_spent,
    CASE 
        WHEN @TotalSpent > 5000 THEN 'VIP Customer'
        ELSE 'Regular Customer'
    END AS customer_status;
--2. Product Price Threshold Report#
--Create a query using variables to count how many products cost more than $1500. Store the threshold price in a variable and display both the threshold and count in a formatted message.
DECLARE @PriceThreshold DECIMAL(10,2) = 1500;
DECLARE @ProductCount INT;

SELECT @ProductCount = COUNT(*)
FROM production.products
WHERE list_price > @PriceThreshold;

SELECT 
    CONCAT('Price Threshold: $', @PriceThreshold) AS threshold_info,
    CONCAT('Number of products above threshold: ', @ProductCount) AS count_info;
--3. Staff Performance Calculator#
--Write a query that calculates the total sales for staff member ID 2 in the year 2017. Use variables to store the staff ID, year, and calculated total. Display the results with appropriate labels.
DECLARE @StaffID INT = 2;
DECLARE @Year INT = 2017;
DECLARE @TotalSales DECIMAL(10,2);

SELECT @TotalSales = SUM(oi.quantity * oi.list_price * (1 - oi.discount))
FROM sales.orders o
JOIN sales.order_items oi ON o.order_id = oi.order_id
WHERE o.staff_id = @StaffID AND YEAR(o.order_date) = @Year;

SELECT 
    @StaffID AS staff_id,
    @Year AS year,
    @TotalSales AS total_sales,
    CONCAT('Staff #', @StaffID, ' generated $', FORMAT(@TotalSales, 'N2'), ' in ', @Year) AS performance_summary;
--4. Global Variables Information#
--Create a query that displays the current server name, SQL Server version, and the number of rows affected by the last statement. Use appropriate global variables.
SELECT 
    @@SERVERNAME AS server_name,
    @@VERSION AS sql_server_version,
    @@ROWCOUNT AS rows_affected_by_last_statement;
--5.Write a query that checks the inventory level for product ID 1 in store ID 1. Use IF statements to display different messages based on stock levels:#
--If quantity > 20: Well stocked
--If quantity 10-20: Moderate stock
--If quantity < 10: Low stock - reorder needed
DECLARE @ProductID INT = 1;
DECLARE @StoreID INT = 1;
DECLARE @Quantity INT;

SELECT @Quantity = quantity
FROM production.stocks
WHERE product_id = @ProductID AND store_id = @StoreID;

IF @Quantity > 20
    PRINT 'Well stocked';
ELSE IF @Quantity BETWEEN 10 AND 20
    PRINT 'Moderate stock';
ELSE IF @Quantity < 10
    PRINT 'Low stock - reorder needed';
ELSE
    PRINT 'No stock data available';
--6.Create a WHILE loop that updates low-stock items (quantity < 5) in batches of 3 products at a time. Add 10 units to each product and display progress messages after each batch.#
DECLARE @BatchSize INT = 3;
DECLARE @Processed INT = 1;

WHILE @Processed > 0
BEGIN
    UPDATE TOP (@BatchSize) production.stocks
    SET quantity = quantity + 10
    OUTPUT inserted.product_id, inserted.quantity
    WHERE quantity < 5;
    
    SET @Processed = @@ROWCOUNT;
    
    IF @Processed > 0
        PRINT CONCAT('Processed batch of ', @Processed, ' products');
END
PRINT 'Low-stock update complete';
GO
--7. Product Price Categorization#
--Write a query that categorizes all products using CASE WHEN based on their list price:
--Under $300: Budget
--$300-$800: Mid-Range
--$801-$2000: Premium
--Over $2000: Luxury
SELECT 
    product_id,
    product_name,
    list_price,
    CASE 
        WHEN list_price < 300 THEN 'Budget'
        WHEN list_price BETWEEN 300 AND 800 THEN 'Mid-Range'
        WHEN list_price BETWEEN 801 AND 2000 THEN 'Premium'
        WHEN list_price > 2000 THEN 'Luxury'
    END AS price_category
FROM production.products;
--8. Customer Order Validation#
--Create a query that checks if customer ID 5 exists in the database. If they exist, show their order count. If not, display an appropriate message.
DECLARE @CustomerID INT = 5;
IF EXISTS (SELECT 1 FROM sales.customers WHERE customer_id = @CustomerID)
BEGIN
    SELECT 
        c.customer_id,
        c.first_name,
        c.last_name,
        COUNT(o.order_id) AS order_count
    FROM sales.customers c
    LEFT JOIN sales.orders o ON c.customer_id = o.customer_id
    WHERE c.customer_id = @CustomerID
    GROUP BY c.customer_id, c.first_name, c.last_name;
END
ELSE
    PRINT 'Customer ID ' + CAST(@CustomerID AS VARCHAR) + ' does not exist';
GO
--9. Shipping Cost Calculator Function#
--Create a scalar function named CalculateShipping that takes an order total as input and returns shipping cost:
--Orders over $100: Free shipping ($0)
--Orders $50-$99: Reduced shipping ($5.99)
--Orders under $50: Standard shipping ($12.99)
CREATE FUNCTION dbo.CalculateShipping(@OrderTotal DECIMAL(10,2))
RETURNS DECIMAL(10,2)
AS
BEGIN
    RETURN CASE 
        WHEN @OrderTotal > 100 THEN 0.00
        WHEN @OrderTotal BETWEEN 50 AND 99 THEN 5.99
        ELSE 12.99
    END;
END;
GO
--10. Product Category Function#
--Create an inline table-valued function named GetProductsByPriceRange that accepts minimum and maximum price parameters and returns all products within that price range with their brand and category information.
CREATE FUNCTION dbo.GetProductsByPriceRange(@MinPrice DECIMAL(10,2), @MaxPrice DECIMAL(10,2))
RETURNS TABLE
AS
RETURN (
    SELECT 
        p.product_id,
        p.product_name,
        p.list_price,
        b.brand_name,
        c.category_name
    FROM production.products p
    JOIN production.brands b ON p.brand_id = b.brand_id
    JOIN production.categories c ON p.category_id = c.category_id
    WHERE p.list_price BETWEEN @MinPrice AND @MaxPrice
);
GO
--11. Customer Sales Summary Function#
--Create a multi-statement function named GetCustomerYearlySummary that takes a customer ID and returns a table with yearly sales data including total orders, total spent, and average order value for each year.
CREATE FUNCTION dbo.GetCustomerYearlySummary(@CustomerID INT)
RETURNS @Summary TABLE (
    year INT,
    order_count INT,
    total_spent DECIMAL(10,2),
    avg_order_value DECIMAL(10,2))
AS
BEGIN
    INSERT INTO @Summary
    SELECT 
        YEAR(o.order_date) AS year,
        COUNT(DISTINCT o.order_id) AS order_count,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) / COUNT(DISTINCT o.order_id) AS avg_order_value
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    WHERE o.customer_id = @CustomerID
    GROUP BY YEAR(o.order_date);
    
    RETURN;
END;
GO
--12. Discount Calculation Function#
--Write a scalar function named CalculateBulkDiscount that determines discount percentage based on quantity:
--1-2 items: 0% discount
--3-5 items: 5% discount
--6-9 items: 10% discount
--10+ items: 15% discount
CREATE FUNCTION dbo.CalculateBulkDiscount(@Quantity INT)
RETURNS DECIMAL(4,2)
AS
BEGIN
    RETURN CASE 
        WHEN @Quantity BETWEEN 1 AND 2 THEN 0.00
        WHEN @Quantity BETWEEN 3 AND 5 THEN 5.00
        WHEN @Quantity BETWEEN 6 AND 9 THEN 10.00
        WHEN @Quantity >= 10 THEN 15.00
        ELSE 0.00
    END;
END;
GO
--13. Customer Order History Procedure#
--Create a stored procedure named sp_GetCustomerOrderHistory that accepts a customer ID and optional start/end dates. Return the customer's order history with order totals calculated.
CREATE PROCEDURE sp_GetCustomerOrderHistory
    @CustomerID INT,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
    SET @StartDate = ISNULL(@StartDate, '1900-01-01');
    SET @EndDate = ISNULL(@EndDate, GETDATE());
    
    SELECT 
        o.order_id,
        o.order_date,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS order_total
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    WHERE o.customer_id = @CustomerID
    AND o.order_date BETWEEN @StartDate AND @EndDate
    GROUP BY o.order_id, o.order_date
    ORDER BY o.order_date DESC;
END;
GO
-- 14. Inventory Restock Procedure#
--Write a stored procedure named sp_RestockProduct with input parameters for store ID, product ID, and restock quantity. 
--Include output parameters for old quantity, new quantity, and success status.

CREATE PROCEDURE sp_RestockProduct
                 @store_id INT = 0,
     @product_id INT = 0,
     @restock_quantity DECIMAL(10,2) =0.00
AS 
BEGIN
     DECLARE @old_quantity DECIMAL(10,2) =0.00;
  DECLARE @new_quantity DECIMAL(10,2) =0.00;
  DECLARE @success_status INT = 0;
     SELECT 
        @old_quantity = quantity
  FROM production.stocks
  WHERE store_id = @store_id AND product_id = @product_id;

   IF @old_quantity IS NOT NULL
    BEGIN
        SET @new_quantity = @old_quantity + @restock_quantity;

        UPDATE production.stocks
        SET quantity = @new_quantity
        WHERE store_id = @store_id AND product_id = @product_id;
        SET @success_status = 1;
    END
    ELSE
    BEGIN
        SET @old_quantity = 0;
        SET @new_quantity = 0;
        SET @success_status = 0;
    END
 
    SELECT @old_quantity AS Old_Quantity,
       @new_quantity AS New_Quantity,
       @success_status AS Success_Status;
END;
GO
EXEC sp_RestockProduct 
    @store_id = 2, 
    @product_id = 100, 
    @restock_quantity = 20.00;
GO
--15. Order Processing Procedure#
--Create a stored procedure named sp_ProcessNewOrder that handles complete order creation with proper transaction control and error handling. Include parameters for customer ID, product ID, quantity, and store ID.
CREATE PROCEDURE sp_ProcessNewOrder
    @CustomerID INT,
    @ProductID INT,
    @Quantity INT,
    @StoreID INT,
    @OrderID INT OUTPUT,
    @Success BIT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Check customer exists
        IF NOT EXISTS (SELECT 1 FROM sales.customers WHERE customer_id = @CustomerID)
        BEGIN
            SET @Success = 0;
            SET @Message = 'Customer does not exist';
            ROLLBACK;
            RETURN;
        END
        
        -- Check product exists
        IF NOT EXISTS (SELECT 1 FROM production.products WHERE product_id = @ProductID)
        BEGIN
            SET @Success = 0;
            SET @Message = 'Product does not exist';
            ROLLBACK;
            RETURN;
        END
        
        -- Check store exists
        IF NOT EXISTS (SELECT 1 FROM sales.stores WHERE store_id = @StoreID)
        BEGIN
            SET @Success = 0;
            SET @Message = 'Store does not exist';
            ROLLBACK;
            RETURN;
        END
        
        -- Check inventory
        DECLARE @AvailableQty INT;
        SELECT @AvailableQty = quantity 
        FROM production.stocks 
        WHERE product_id = @ProductID AND store_id = @StoreID;
        
        IF @AvailableQty < @Quantity
        BEGIN
            SET @Success = 0;
            SET @Message = CONCAT('Insufficient inventory. Available: ', ISNULL(@AvailableQty, 0));
            ROLLBACK;
            RETURN;
        END
        
        -- Create order
        DECLARE @OrderDate DATE = GETDATE();
        DECLARE @Status TINYINT = 1; -- Order Received
        
        INSERT INTO sales.orders (
            customer_id, order_status, order_date, 
            required_date, store_id, staff_id
        )
        VALUES (
            @CustomerID, @Status, @OrderDate,
            DATEADD(day, 7, @OrderDate), @StoreID, 1 -- Assuming staff_id 1
        );
        
        SET @OrderID = SCOPE_IDENTITY();
        
        -- Add order item
        DECLARE @ListPrice DECIMAL(10,2);
        DECLARE @Discount DECIMAL(4,2) = dbo.CalculateBulkDiscount(@Quantity);
        
        SELECT @ListPrice = list_price
        FROM production.products
        WHERE product_id = @ProductID;
        
        INSERT INTO sales.order_items (
            order_id, item_id, product_id, 
            quantity, list_price, discount
        )
        VALUES (
            @OrderID, 1, @ProductID,
            @Quantity, @ListPrice, @Discount
        );
        
        -- Update inventory
        UPDATE production.stocks
        SET quantity = quantity - @Quantity
        WHERE product_id = @ProductID AND store_id = @StoreID;
        
        SET @Success = 1;
        SET @Message = 'Order processed successfully';
        COMMIT;
    END TRY
    BEGIN CATCH
        SET @Success = 0;
        SET @Message = ERROR_MESSAGE();
        ROLLBACK;
    END CATCH
END;
GO
--16. Dynamic Product Search Procedure#
--Write a stored procedure named sp_SearchProducts that builds dynamic SQL based on optional parameters: product name search term, category ID, minimum price, maximum price, and sort column.
CREATE OR ALTER PROCEDURE sp_SearchProducts
    @name VARCHAR(100) = NULL,
    @category_id INT = NULL,
    @min_price DECIMAL(10,2) = NULL,
    @max_price DECIMAL(10,2) = NULL,
    @sort_column VARCHAR(50) = 'product_name'
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Validate sort column to prevent SQL injection
    IF @sort_column NOT IN ('product_id', 'product_name', 'category_id', 'list_price')
        SET @sort_column = 'product_name';
    
    -- Build the dynamic SQL safely
    DECLARE @sql NVARCHAR(MAX) = N'
    SELECT 
        p.product_id,
        p.product_name,
        p.category_id,
        c.category_name,
        p.list_price,
        b.brand_name
    FROM 
        production.products p
        JOIN production.categories c ON p.category_id = c.category_id
        JOIN production.brands b ON p.brand_id = b.brand_id
    WHERE 1=1';
    
    -- Add conditions for each parameter
    IF @name IS NOT NULL
        SET @sql = @sql + N' AND p.product_name LIKE ''%' + REPLACE(@name, '''', '''''') + '%''';
    
    IF @category_id IS NOT NULL
        SET @sql = @sql + N' AND p.category_id = ' + CAST(@category_id AS NVARCHAR(10));
    
    IF @min_price IS NOT NULL
        SET @sql = @sql + N' AND p.list_price >= ' + CAST(@min_price AS NVARCHAR(20));
    
    IF @max_price IS NOT NULL
        SET @sql = @sql + N' AND p.list_price <= ' + CAST(@max_price AS NVARCHAR(20));
    
    -- Add sorting
    SET @sql = @sql + N' ORDER BY ' + QUOTENAME(@sort_column);
    
    -- Execute the dynamic SQL
    EXEC sp_executesql @sql;
END;
-- Simple test
GO
EXEC sp_SearchProducts @name = 'bike';
GO
-- Full parameter test
EXEC sp_SearchProducts 
    @name = 'mountain',
    @category_id = 5,
    @min_price = 500,
    @max_price = 2000,
    @sort_column = 'list_price';
--17. Staff Bonus Calculation System#
--Create a complete solution that calculates quarterly bonuses for all staff members. Use variables to store date ranges and bonus rates. Apply different bonus percentages based on sales performance tiers.
DECLARE @StartDate DATE = '2017-01-01';
DECLARE @EndDate DATE = '2017-03-31';
DECLARE @BaseBonusRate DECIMAL(5,2) = 0.05; -- 5% base
DECLARE @PerformanceMultipliers TABLE (
    tier INT,
    min_sales DECIMAL(10,2),
    max_sales DECIMAL(10,2),
    multiplier DECIMAL(5,2)
);

INSERT INTO @PerformanceMultipliers VALUES
(1, 0, 4999.99, 1.0),    -- Standard
(2, 5000, 9999.99, 1.2), -- Bronze
(3, 10000, 19999.99, 1.5), -- Silver
(4, 20000, 39999.99, 2.0), -- Gold
(5, 40000, 9999999, 2.5);  -- Platinum

WITH StaffSales AS (
    SELECT 
        s.staff_id,
        s.first_name,
        s.last_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales
    FROM sales.staffs s
    JOIN sales.orders o ON s.staff_id = o.staff_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    WHERE o.order_date BETWEEN @StartDate AND @EndDate
    GROUP BY s.staff_id, s.first_name, s.last_name
)
SELECT 
    ss.staff_id,
    ss.first_name,
    ss.last_name,
    ss.total_sales,
    pm.tier,
    CASE pm.tier
        WHEN 1 THEN 'Standard'
        WHEN 2 THEN 'Bronze'
        WHEN 3 THEN 'Silver'
        WHEN 4 THEN 'Gold'
        WHEN 5 THEN 'Platinum'
    END AS performance_tier,
    @BaseBonusRate * pm.multiplier AS bonus_rate,
    ss.total_sales * @BaseBonusRate * pm.multiplier AS bonus_amount
FROM StaffSales ss
JOIN @PerformanceMultipliers pm ON ss.total_sales BETWEEN pm.min_sales AND pm.max_sales
ORDER BY ss.total_sales DESC;
--18. Smart Inventory Management#
--Write a complex query with nested IF statements that manages inventory restocking. Check current stock levels and apply different reorder quantities based on product categories and current stock levels.
DECLARE @RestockThresholds TABLE (
    category_id INT,
    category_name VARCHAR(255),
    min_stock INT,
    reorder_qty INT
);

INSERT INTO @RestockThresholds VALUES
(1, 'Category A', 10, 25),
(2, 'Category B', 15, 30),
(3, 'Category C', 5, 20),
(4, 'Category D', 20, 40);

SELECT 
    p.product_id,
    p.product_name,
    c.category_name,
    s.store_id,
    s.quantity AS current_stock,
    rt.min_stock,
    rt.reorder_qty,
    CASE 
        WHEN s.quantity IS NULL THEN 'Not in stock system'
        WHEN s.quantity = 0 THEN 'Out of stock'
        WHEN s.quantity < rt.min_stock THEN 'Low stock - needs reorder'
        ELSE 'Adequate stock'
    END AS inventory_status,
    CASE 
        WHEN s.quantity IS NULL OR s.quantity < rt.min_stock THEN rt.reorder_qty
        ELSE 0
    END AS suggested_reorder_qty
FROM production.products p
JOIN production.categories c ON p.category_id = c.category_id
JOIN @RestockThresholds rt ON c.category_id = rt.category_id
LEFT JOIN production.stocks s ON p.product_id = s.product_id
WHERE (s.quantity IS NULL OR s.quantity < rt.min_stock)
ORDER BY c.category_name, p.product_name;
--19. Customer Loyalty Tier Assignment#
--Create a comprehensive solution that assigns loyalty tiers to customers based on their total spending. Handle customers with no orders appropriately and use proper NULL checking.
WITH CustomerSpending AS (
    SELECT 
        c.customer_id,
        c.first_name,
        c.last_name,
        COALESCE(SUM(oi.quantity * oi.list_price * (1 - oi.discount)), 0) AS total_spent,
        COUNT(DISTINCT o.order_id) AS order_count
    FROM sales.customers c
    LEFT JOIN sales.orders o ON c.customer_id = o.customer_id
    LEFT JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY c.customer_id, c.first_name, c.last_name
)
SELECT 
    customer_id,
    first_name,
    last_name,
    total_spent,
    order_count,
    CASE 
        WHEN total_spent = 0 THEN 'No Orders'
        WHEN total_spent < 1000 THEN 'Bronze'
        WHEN total_spent BETWEEN 1000 AND 4999 THEN 'Silver'
        WHEN total_spent BETWEEN 5000 AND 9999 THEN 'Gold'
        WHEN total_spent >= 10000 THEN 'Platinum'
    END AS loyalty_tier,
    CASE 
        WHEN total_spent = 0 THEN 'No benefits'
        WHEN total_spent < 1000 THEN '5% discount on next order'
        WHEN total_spent BETWEEN 1000 AND 4999 THEN '10% discount + free shipping'
        WHEN total_spent BETWEEN 5000 AND 9999 THEN '15% discount + priority support'
        WHEN total_spent >= 10000 THEN '20% discount + personal account manager'
    END AS tier_benefits
FROM CustomerSpending
ORDER BY total_spent DESC;
GO
--20.Product Lifecycle Management#
--Write a stored procedure that handles product discontinuation including checking for pending orders, 
--optional product replacement in existing orders, clearing inventory, and providing detailed status messages.
CREATE PROCEDURE sp_DiscontinueProduct
    @product_id INT,
    @replacement_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Check if the product exists
    IF NOT EXISTS (SELECT 1 FROM production.products WHERE product_id = @product_id)
    BEGIN
        PRINT 'Product not found.';
        RETURN;
    END
    -- Check if there are any pending orders (status = 1)
    IF EXISTS (
        SELECT 1
        FROM sales.orders o
        JOIN sales.order_items oi ON o.order_id = oi.order_id
        WHERE oi.product_id = @product_id AND o.order_status = 1
    )
    BEGIN
        -- If a replacement product is provided
        IF @replacement_id IS NOT NULL
        BEGIN
            -- Check if the replacement product exists
            IF NOT EXISTS (SELECT 1 FROM production.products WHERE product_id = @replacement_id)
            BEGIN
                PRINT 'Replacement product not found.';
                RETURN;
            END
            -- Replace the product in pending orders
            UPDATE oi
            SET product_id = @replacement_id
            FROM sales.order_items oi
            JOIN sales.orders o ON oi.order_id = o.order_id
            WHERE oi.product_id = @product_id AND o.order_status = 1;
            PRINT 'Product replaced in pending orders.';
        END
        ELSE
        BEGIN
            PRINT 'Cannot discontinue: product is in pending orders and no replacement was provided.';
            RETURN;
        END
    END
    --Clear inventory for the discontinued product
    UPDATE production.stocks
    SET quantity = 0
    WHERE product_id = @product_id;
    PRINT 'Inventory cleared for discontinued product.';
END;
GO
EXEC sp_DiscontinueProduct @product_id = 100;
EXEC sp_DiscontinueProduct @product_id = 66, @replacement_id = 205;