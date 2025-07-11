-- Count the total number of products in the database.
SELECT COUNT(product_name) AS 'Total_Products'
FROM production.products;

--Find the average, minimum, and maximum price of all products.
SELECT AVG(list_price) AS "Average_Price" , MIN(list_price) AS "Min_Price" ,MAX(list_price) AS 'Max_Price' 
FROM production.products;

--Count how many products are in each category.
SELECT c.category_name, COUNT(p.product_id) AS 'total_products'
FROM production.categories c
JOIN production.products p 
ON c.category_id = p.category_id
GROUP BY c.category_name;

-- Find the total number of orders for each store.
SELECT store_id, COUNT(order_id) AS total_orders
FROM sales.orders
GROUP BY store_id;

-- Show customer first names in UPPERCASE and last names in lowercase for the first 10 customers.
SELECT TOP(10) UPPER(first_name) AS 'First_Name' ,LOWER(last_name) AS 'last_name'
FROM sales.customers;

--Get the length of each product name. Show product name and its length for the first 10 products.
SELECT TOP(10) product_name, LEN(product_name) AS " Len_Of_Product"
FROM production.products;

-- Format customer phone numbers to show only the area code (first 3 digits) for customers 1-15.
SELECT TOP(15) Left(phone,3) AS "Area_Code"
FROM sales.customers;

--Show the current date and extract the year and month from order dates for orders 1-10.
SELECT order_date, YEAR(order_date) AS 'Year' ,MONTH(order_date) AS 'Month'
FROM sales.orders;

--Join products with their categories. Show product name and category name for first 10 products.
SELECT TOP(10) p.product_name,c.category_name
FROM production.products p 
JOIN production.categories c 
ON p.category_id=c.category_id;

-- Join customers with their orders. Show customer name and order date for first 10 orders.
SELECT TOP(10) c.first_name+' '+c.last_name AS "Full Name" , o.order_date
FROM sales.customers c 
JOIN sales.orders o
ON c.customer_id=o.customer_id;

--Show all products with their brand names, even if some products don't have brands. Include product name, brand name (show 'No Brand' if null).
SELECT p.product_name,
       COALESCE(b.brand_name, 'No brand ') AS 'Brand Name'
FROM production.products p 
  left JOIN production.brands b
ON p.brand_id=b.brand_id;

--Find products that cost more than the average product price. Show product name and price.
SELECT product_name, list_price
FROM production.products
WHERE list_price >(
      SELECT AVG(list_price)
	  FROM production.products
	  );

--Find customers who have placed at least one order. Use a subquery with IN. Show customer_id and customer_name.
SELECT customer_id,first_name+' '+last_name AS  'Full Name'
FROM sales.customers
WHERE customer_id IN (
        SELECT customer_id
        FROM sales.orders
        WHERE customer_id IS NOT NULL );

-- For each customer, show their name and total number of orders using a subquery in the SELECT clause.
SELECT 
    c.customer_id,c.first_name + ' ' + c.last_name AS 'Full Name',
    (   SELECT COUNT(o.order_id) 
        FROM sales.orders o 
        WHERE o.customer_id = c.customer_id) AS total_orders
FROM sales.customers c
group by c.customer_id,first_name,last_name;


-- Create a simple view called easy_product_list that shows product name, category name, and price. Then write a query to select all products from this view where price > 100.
CREATE VIEW easy_product_list AS
SELECT
    p.product_name,
    c.category_name,
    p.list_price
FROM production.products p
JOIN production.categories c ON p.category_id = c.category_id;


-- write a query to select all products from this view where price > 100.
SELECT * FROM easy_product_list
WHERE list_price > 100;


--Create a view called customer_info that shows customer ID, full name (first + last), email, and city and state combined. Then use this view to find all customers from California (CA).

CREATE VIEW customer_info AS
SELECT
    customer_id,
    first_name + ' ' + last_name as 'full name',
    email,
    city,
    state
FROM sales.customers;

-- use this view to find all customers from California (CA).
SELECT * FROM customer_info
WHERE state = 'CA'
ORDER BY 'full name';

--Find all products that cost between $50 and $200. Show product name and price, ordered by price from lowest to highest.
SELECT product_name, list_price
FROM production.products
WHERE list_price between 50 AND 200 
ORDER BY list_price;

--Count how many customers live in each state. Show state and customer count, ordered by count from highest to lowest.
SELECT state,COUNT(customer_id) AS 'Customer Count'
FROM sales.customers
GROUP BY state
ORDER BY 'Customer Count' DESC;

-- Find the most expensive product in each category. Show category name, product name, and price.
SELECT c.category_name, p.product_name, p.list_price
FROM  production.products p
JOIN  production.categories c 
ON p.category_id = c.category_id
WHERE p.list_price = (
        SELECT MAX(pp.list_price)
        FROM production.products pp
        WHERE pp.category_id = p.category_id);

-- Show all stores and their cities, including the total number of orders from each store. Show store name, city, and order count.
SELECT  s.store_name, s.city, COUNT(o.order_id) AS 'order_count'
FROM  sales.stores s 
left JOIN sales.orders o 
ON s.store_id = o.store_id
GROUP BY s.store_name, s.city;