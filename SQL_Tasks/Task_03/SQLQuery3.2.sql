--List all products with list price greater than 1000
SELECT product_name,list_price    FROM production.products WHERE list_price>1000

-- Get customers from "CA" or "NY" states
SELECT first_name + ' ' +last_name as "Full Name" ,state
FROM sales.customers
Where State in('NY','CA');

-- Retrieve all orders placed in 2023
SELECT *
FROM sales.orders
WHERE YEAR(order_date) = 2023;

--Show customers whose emails end with @gmail.com
SELECT * FROM sales.customers WHERE RIGHT(email,10)='@gmail.com'

--Show all inactive staff
SELECT first_name+ ' '+ last_name AS "Staff Name" 
FROM sales.staffs
WHERE active=0;

--List top 5 most expensive products
SELECT TOP (5) product_name
FROM production.products
Order by list_price DESC

-- Show latest 10 orders sorted by date
SELECT TOP (10) * FROM sales.orders
ORDER BY order_date DESC

--Retrieve the first 3 customers alphabetically by last name
SELECT top(3)first_name+ ' '+ last_name AS "Customer_Name" 
FROM sales.customers
ORDER BY last_name ASC

-- Find customers who did not provide a phone number
SELECT first_name+ ' '+ last_name AS "Full Name" 
FROM sales.customers
WHERE phone is NULL;

--Show all staff who have a manager assigned
SELECT first_name+ ' '+ last_name AS "S_Name"
FROM sales.staffs
WHERE manager_id is NOT NULL;

-- Count number of products in each category
SELECT 
  count(p.product_id) as'Num of products',
    c.category_name
FROM production.categories c
LEFT JOIN  production.products p ON p.category_id = c.category_id
GROUP BY c.category_name
ORDER BY  'Num of products' DESC, c.category_name;

--12) Count number of customers in each state
SELECT state,COUNT(customer_id) AS 'Num Of Customers'
FROM sales.customers
WHERE state IS NOT NULL
GROUP BY state;


-- Get average list price of products per brand
SELECT AVG(p.list_price) AS "AVG_List_Price",b.brand_name
FROM production.products p JOIN production.brands b ON
p.brand_id=b.brand_id
GROUP BY brand_name

-- Show number of orders per staff
SELECT  s.first_name+ ' '+ s.last_name AS "S_Name", count(o.order_id) AS "Num Of Orders" 
FROM sales.orders o
JOIN sales.staffs s ON o.staff_id=s.staff_id
GROUP BY s.first_name ,s.last_name

-- Find customers who made more than 2 orders
SELECT  c.first_name+ ' '+ c.last_name AS "Full Name" ,count (o.order_id) AS "Num of Orders"
FROM sales.customers c JOIN sales.orders o on c.customer_id=o.customer_id
GROUP BY c.first_name,c.last_name
HAVING COUNT(o.order_id) > 2;

-- Products priced between 500 and 1500
SELECT product_name,list_price 
FROM production.products
WHERE list_price between 500 AND 1500

--Customers in cities starting with "S"
SELECT  first_name+ ' '+ last_name AS "Full Name" ,city
FROM sales.customers 
WHERE city like 'S%'

-- Orders with order_status either 2 or 4
SELECT order_id,order_status
FROM sales.orders
WHERE order_status in (2,4)  

-- Products from category_id IN (1, 2, 3)
SELECT *
FROM production.products
WHERE category_id IN (1, 2, 3);

-- Staff working in store_id = 1 OR without phone number
SELECT  first_name+ ' '+ last_name AS "S_NAME" 
FROM sales.staffs
WHERE store_id =1 OR phone IS NULL