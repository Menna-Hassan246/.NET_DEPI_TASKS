--1.Write a query that classifies all products into price categories:
--Products under $300: "Economy"
--Products $300-$999: "Standard"
--Products $1000-$2499: "Premium"
--Products $2500 and above: "Luxury"
SELECT 
    product_id,
    product_name,
    list_price,
    CASE 
        WHEN list_price < 300 THEN 'Economy'
        WHEN list_price BETWEEN 300 AND 999 THEN 'Standard'
        WHEN list_price BETWEEN 1000 AND 2499 THEN 'Premium'
        WHEN list_price >= 2500 THEN 'Luxury'
    END AS price_category
FROM production.products;

--2.Create a query that shows order processing information with user-friendly status descriptions:

--Status 1: "Order Received"
--Status 2: "In Preparation"
--Status 3: "Order Cancelled"
--Status 4: "Order Delivered"
--Also add a priority level:

--Orders with status 1 older than 5 days: "URGENT"
--Orders with status 2 older than 3 days: "HIGH"
--All other orders: "NORMAL"
SELECT 
    o.order_id,
    o.order_date,
    CASE o.order_status
        WHEN 1 THEN 'Order Received'
        WHEN 2 THEN 'In Preparation'
        WHEN 3 THEN 'Order Cancelled'
        WHEN 4 THEN 'Order Delivered'
    END AS status_description,
    CASE 
        WHEN o.order_status = 1 AND DATEDIFF(day, o.order_date, GETDATE()) > 5 THEN 'URGENT'
        WHEN o.order_status = 2 AND DATEDIFF(day, o.order_date, GETDATE()) > 3 THEN 'HIGH'
        ELSE 'NORMAL'
    END AS priority_level
FROM sales.orders o;
--3.Write a query that categorizes staff based on the number of orders they've handled:

--0 orders: "New Staff"
--1-10 orders: "Junior Staff"
--11-25 orders: "Senior Staff"
--26+ orders: "Expert Staff"
SELECT 
    s.staff_id,
    s.first_name,
    s.last_name,
    COUNT(o.order_id) AS orders_handled,
    CASE 
        WHEN COUNT(o.order_id) = 0 THEN 'New Staff'
        WHEN COUNT(o.order_id) BETWEEN 1 AND 10 THEN 'Junior Staff'
        WHEN COUNT(o.order_id) BETWEEN 11 AND 25 THEN 'Senior Staff'
        WHEN COUNT(o.order_id) >= 26 THEN 'Expert Staff'
    END AS staff_category
FROM sales.staffs s
LEFT JOIN sales.orders o ON s.staff_id = o.staff_id
GROUP BY s.staff_id, s.first_name, s.last_name;
--4.Create a query that handles missing customer contact information:
--Use ISNULL to replace missing phone numbers with "Phone Not Available"
--Use COALESCE to create a preferred_contact field (phone first, then email, then "No Contact Method")
--Show complete customer information
SELECT 
    customer_id,
    first_name,
    last_name,
    ISNULL(phone, 'Phone Not Available') AS phone,
    email,
    COALESCE(phone, email, 'No Contact Method') AS preferred_contact,
    street,
    city,
    state,
    zip_code
FROM sales.customers;
--5.Write a query that safely calculates price per unit in stock:
--Use NULLIF to prevent division by zero when quantity is 0
--Use ISNULL to show 0 when no stock exists
--Include stock status using CASE WHEN
--Only show products from store_id = 1
SELECT 
    p.product_id,
    p.product_name,
    p.list_price,
    ISNULL(s.quantity, 0) AS quantity,
    CASE 
        WHEN ISNULL(s.quantity, 0) = 0 THEN 'Out of Stock'
        ELSE 'In Stock'
    END AS stock_status,
    p.list_price / NULLIF(s.quantity, 0) AS price_per_unit
FROM production.products p
LEFT JOIN production.stocks s ON p.product_id = s.product_id AND s.store_id = 1
WHERE s.store_id = 1 OR s.store_id IS NULL;
--6.Create a query that formats complete addresses safely:
--Use COALESCE for each address component
--Create a formatted_address field that combines all components
--Handle missing ZIP codes gracefully
SELECT 
    customer_id,
    first_name,
    last_name,
    COALESCE(
        street + ', ' + 
        city + ', ' + 
        state + ' ' + 
        COALESCE(zip_code, ''),
        'Address not available'
    ) AS formatted_address
FROM sales.customers;
--7.Use a CTE to find customers who have spent more than $1,500 total:
--Create a CTE that calculates total spending per customer
--Join with customer information
--Show customer details and spending
--Order by total_spent descending
WITH CustomerSpending AS (
    SELECT 
        o.customer_id,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY o.customer_id
    HAVING SUM(oi.quantity * oi.list_price * (1 - oi.discount)) > 1500
)
SELECT 
    c.customer_id,
    c.first_name,
    c.last_name,
    c.email,
    c.phone,
    cs.total_spent
FROM sales.customers c
JOIN CustomerSpending cs ON c.customer_id = cs.customer_id
ORDER BY cs.total_spent DESC;
--8.Create a multi-CTE query for category analysis:
--CTE 1: Calculate total revenue per category
--CTE 2: Calculate average order value per category
--Main query: Combine both CTEs
--Use CASE to rate performance: >$50000 = "Excellent", >$20000 = "Good", else = "Needs Improvement"
WITH CategoryRevenue AS (
    SELECT 
        c.category_id,
        c.category_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue
    FROM production.categories c
    JOIN production.products p ON c.category_id = p.category_id
    JOIN sales.order_items oi ON p.product_id = oi.product_id
    GROUP BY c.category_id, c.category_name
),
CategoryAvgOrder AS (
    SELECT 
        c.category_id,
        AVG(oi.quantity * oi.list_price * (1 - oi.discount)) AS avg_order_value
    FROM production.categories c
    JOIN production.products p ON c.category_id = p.category_id
    JOIN sales.order_items oi ON p.product_id = oi.product_id
    GROUP BY c.category_id
)
SELECT 
    cr.category_id,
    cr.category_name,
    cr.total_revenue,
    cao.avg_order_value,
    CASE 
        WHEN cr.total_revenue > 50000 THEN 'Excellent'
        WHEN cr.total_revenue > 20000 THEN 'Good'
        ELSE 'Needs Improvement'
    END AS performance_rating
FROM CategoryRevenue cr
JOIN CategoryAvgOrder cao ON cr.category_id = cao.category_id;
--9.Use CTEs to analyze monthly sales trends:
--CTE 1: Calculate monthly sales totals
--CTE 2: Add previous month comparison
--Show growth percentage
WITH MonthlySales AS (
    SELECT 
        YEAR(order_date) AS year,
        MONTH(order_date) AS month,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS monthly_sales
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY YEAR(order_date), MONTH(order_date)
),
SalesWithPrevious AS (
    SELECT 
        year,
        month,
        monthly_sales,
        LAG(monthly_sales, 1) OVER (ORDER BY year, month) AS previous_month_sales
    FROM MonthlySales
)
SELECT 
    year,
    month,
    monthly_sales,
    previous_month_sales,
    CASE 
        WHEN previous_month_sales IS NULL THEN NULL
        ELSE (monthly_sales - previous_month_sales) / previous_month_sales * 100
    END AS growth_percentage
FROM SalesWithPrevious;
--10.Create a query that ranks products within each category:
--Use ROW_NUMBER() to rank by price (highest first)
--Use RANK() to handle ties
--Use DENSE_RANK() for continuous ranking
--Only show top 3 products per category
WITH RankedProducts AS (
    SELECT 
        c.category_name,
        p.product_name,
        p.list_price,
        ROW_NUMBER() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS row_num,
        RANK() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS price_rank,
        DENSE_RANK() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS dense_price_rank
    FROM production.products p
    JOIN production.categories c ON p.category_id = c.category_id
)
SELECT 
    category_name,
    product_name,
    list_price,
    price_rank
FROM RankedProducts
WHERE row_num <= 3;
--11.Rank customers by their total spending:
--Calculate total spending per customer
--Use RANK() for customer ranking
--Use NTILE(5) to divide into 5 spending groups
--Use CASE for tiers: 1="VIP", 2="Gold", 3="Silver", 4="Bronze", 5="Standard"
WITH CustomerSpending AS (
    SELECT 
        c.customer_id,
        c.first_name,
        c.last_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_spent,
        RANK() OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount)) DESC) AS spending_rank,
        NTILE(5) OVER (ORDER BY SUM(oi.quantity * oi.list_price * (1 - oi.discount)) DESC) AS spending_group
    FROM sales.customers c
    JOIN sales.orders o ON c.customer_id = o.customer_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY c.customer_id, c.first_name, c.last_name
)
SELECT 
    customer_id,
    first_name,
    last_name,
    total_spent,
    spending_rank,
    CASE spending_group
        WHEN 1 THEN 'VIP'
        WHEN 2 THEN 'Gold'
        WHEN 3 THEN 'Silver'
        WHEN 4 THEN 'Bronze'
        WHEN 5 THEN 'Standard'
    END AS customer_tier
FROM CustomerSpending;
--12.Create a comprehensive store performance ranking:
--Rank stores by total revenue
--Rank stores by number of orders
--Use PERCENT_RANK() to show percentile performance
WITH StoreMetrics AS (
    SELECT 
        s.store_id,
        s.store_name,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue,
        COUNT(DISTINCT o.order_id) AS order_count
    FROM sales.stores s
    JOIN sales.orders o ON s.store_id = o.store_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY s.store_id, s.store_name
)
SELECT 
    store_id,
    store_name,
    total_revenue,
    RANK() OVER (ORDER BY total_revenue DESC) AS revenue_rank,
    order_count,
    RANK() OVER (ORDER BY order_count DESC) AS order_rank,
    PERCENT_RANK() OVER (ORDER BY total_revenue) AS revenue_percentile
FROM StoreMetrics;
--13.Create a PIVOT table showing product counts by category and brand:
--Rows: Categories
--Columns: Top 4 brands (Electra, Haro, Trek, Surly)
--Values: Count of products
SELECT *
FROM (
    SELECT 
        c.category_name,
        b.brand_name,
        p.product_id
    FROM production.products p
    JOIN production.categories c ON p.category_id = c.category_id
    JOIN production.brands b ON p.brand_id = b.brand_id
) AS SourceTable
PIVOT (
    COUNT(product_id)
    FOR brand_name IN ([Electra], [Haro], [Trek], [Surly])
) AS PivotTable;
--14.Create a PIVOT showing monthly sales revenue by store:
--Rows: Store names
--Columns: Months (Jan through Dec)
--Values: Total revenue
--Add a total column
SELECT *
FROM (
    SELECT 
        s.store_name,
        FORMAT(o.order_date, 'MMM') AS month,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS monthly_revenue
    FROM sales.stores s
    JOIN sales.orders o ON s.store_id = o.store_id
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY s.store_name, FORMAT(o.order_date, 'MMM')
) AS SourceTable
PIVOT (
    SUM(monthly_revenue)
    FOR month IN ([Jan], [Feb], [Mar], [Apr], [May], [Jun], [Jul], [Aug], [Sep], [Oct], [Nov], [Dec])
) AS PivotTable;
--15.PIVOT order statuses across stores:
--Rows: Store names
--Columns: Order statuses (Pending, Processing, Completed, Rejected)
--Values: Count of orders
SELECT *
FROM (
    SELECT 
        s.store_name,
        CASE o.order_status
            WHEN 1 THEN 'Pending'
            WHEN 2 THEN 'Processing'
            WHEN 3 THEN 'Rejected'
            WHEN 4 THEN 'Completed'
        END AS status_name,
        o.order_id
    FROM sales.stores s
    JOIN sales.orders o ON s.store_id = o.store_id
) AS SourceTable
PIVOT (
    COUNT(order_id)
    FOR status_name IN ([Pending], [Processing], [Completed], [Rejected])
) AS PivotTable;
--16.Create a PIVOT comparing sales across years:
--Rows: Brand names
--Columns: Years (2016, 2017, 2018)
--Values: Total revenue
--Include percentage growth calculations
SELECT *
FROM (
    SELECT 
        b.brand_name,
        YEAR(o.order_date) AS order_year,
        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS yearly_revenue
    FROM production.brands b
    JOIN production.products p ON b.brand_id = p.brand_id
    JOIN sales.order_items oi ON p.product_id = oi.product_id
    JOIN sales.orders o ON oi.order_id = o.order_id
    GROUP BY b.brand_name, YEAR(o.order_date)
) AS SourceTable
PIVOT (
    SUM(yearly_revenue)
    FOR order_year IN ([2016], [2017], [2018])
) AS PivotTable;
--17.Use UNION to combine different product availability statuses:
--Query 1: In-stock products (quantity > 0)
--Query 2: Out-of-stock products (quantity = 0 or NULL)
--Query 3: Discontinued products (not in stocks table)
-- In-stock products
SELECT 
    p.product_id,
    p.product_name,
    'In Stock' AS availability_status
FROM production.products p
JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.quantity > 0

UNION

-- Out-of-stock products
SELECT 
    p.product_id,
    p.product_name,
    'Out of Stock' AS availability_status
FROM production.products p
JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.quantity = 0 OR s.quantity IS NULL

UNION

-- Discontinued products
SELECT 
    p.product_id,
    p.product_name,
    'Discontinued' AS availability_status
FROM production.products p
LEFT JOIN production.stocks s ON p.product_id = s.product_id
WHERE s.product_id IS NULL;
--18.Use INTERSECT to find loyal customers:
--Find customers who bought in both 2017 AND 2018
--Show their purchase patterns
-- Customers who bought in 2017
SELECT c.customer_id, c.first_name, c.last_name
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2017

INTERSECT

-- Customers who bought in 2018
SELECT c.customer_id, c.first_name, c.last_name
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2018;
--19.Use multiple set operators to analyze product distribution:
--INTERSECT: Products available in all 3 stores
--EXCEPT: Products available in store 1 but not in store 2
--UNION: Combine above results with different labels
-- Products available in all 3 stores
SELECT p.product_id, p.product_name, 'Available in All Stores' AS distribution_status
FROM production.products p
WHERE EXISTS (SELECT 1 FROM production.stocks s WHERE s.product_id = p.product_id AND s.store_id = 1)
AND EXISTS (SELECT 1 FROM production.stocks s WHERE s.product_id = p.product_id AND s.store_id = 2)
AND EXISTS (SELECT 1 FROM production.stocks s WHERE s.product_id = p.product_id AND s.store_id = 3)

UNION

-- Products available in store 1 but not in store 2
SELECT p.product_id, p.product_name, 'Store 1 Only' AS distribution_status
FROM production.products p
WHERE EXISTS (SELECT 1 FROM production.stocks s WHERE s.product_id = p.product_id AND s.store_id = 1)
AND NOT EXISTS (SELECT 1 FROM production.stocks s WHERE s.product_id = p.product_id AND s.store_id = 2);
--20.Complex set operations for customer retention:
--Find customers who bought in 2016 but not in 2017 (lost customers)
--Find customers who bought in 2017 but not in 2016 (new customers)
--Find customers who bought in both years (retained customers)
--Use UNION ALL to combine all three groups
-- Lost customers (2016 but not 2017)
SELECT c.customer_id, c.first_name, c.last_name, 'Lost Customer' AS customer_status
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2016
AND c.customer_id NOT IN (
    SELECT o2.customer_id 
    FROM sales.orders o2 
    WHERE YEAR(o2.order_date) = 2017
)
UNION ALL
-- New customers (2017 but not 2016)
SELECT c.customer_id, c.first_name, c.last_name, 'New Customer' AS customer_status
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2017
AND c.customer_id NOT IN (
    SELECT o2.customer_id 
    FROM sales.orders o2 
    WHERE YEAR(o2.order_date) = 2016)
UNION ALL
-- Retained customers (both years)
SELECT c.customer_id, c.first_name, c.last_name, 'Retained Customer' AS customer_status
FROM sales.customers c
JOIN sales.orders o ON c.customer_id = o.customer_id
WHERE YEAR(o.order_date) = 2016
AND c.customer_id IN (
    SELECT o2.customer_id 
    FROM sales.orders o2 
    WHERE YEAR(o2.order_date) = 2017
);