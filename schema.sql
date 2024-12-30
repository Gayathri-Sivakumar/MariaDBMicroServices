-- Create the customer database
CREATE DATABASE customer_db;

-- Create the customers table
CREATE TABLE customer_db.`customers` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `email` varchar(50) NOT NULL,
  `phone` varchar(15),
  `address` varchar(255),
  PRIMARY KEY (`id`)
) ENGINE=InnoDB;

-- Create the product database
CREATE DATABASE product_db;

-- Create the products table
CREATE TABLE product_db.`products` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(255) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  `stock` int(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB;

-- Create a categories table for product categorization
CREATE TABLE product_db.`categories` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(100) NOT NULL,
  `description` varchar(255),
  PRIMARY KEY (`id`)
) ENGINE=InnoDB;

-- Create a mapping table to associate products with categories
CREATE TABLE product_db.`product_categories` (
  `product_id` int(11) unsigned NOT NULL,
  `category_id` int(11) unsigned NOT NULL,
  PRIMARY KEY (`product_id`, `category_id`),
  FOREIGN KEY (`product_id`) REFERENCES product_db.`products`(`id`) ON DELETE CASCADE,
  FOREIGN KEY (`category_id`) REFERENCES product_db.`categories`(`id`) ON DELETE CASCADE
) ENGINE=InnoDB;

-- Create an orders table to manage customer orders
CREATE DATABASE order_db;

CREATE TABLE order_db.`orders` (
  `id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `customer_id` int(11) unsigned NOT NULL,
  `order_date` datetime NOT NULL,
  `total_amount` decimal(10,2) NOT NULL,
  `status` varchar(50) NOT NULL DEFAULT 'Pending',
  PRIMARY KEY (`id`),
  FOREIGN KEY (`customer_id`) REFERENCES customer_db.`customers`(`id`) ON DELETE CASCADE
) ENGINE=InnoDB;

-- Create an order_items table to link orders with products
CREATE TABLE order_db.`order_items` (
  `order_id` int(11) unsigned NOT NULL,
  `product_id` int(11) unsigned NOT NULL,
  `quantity` int(11) NOT NULL,
  `price` decimal(10,2) NOT NULL,
  PRIMARY KEY (`order_id`, `product_id`),
  FOREIGN KEY (`order_id`) REFERENCES order_db.`orders`(`id`) ON DELETE CASCADE,
  FOREIGN KEY (`product_id`) REFERENCES product_db.`products`(`id`) ON DELETE CASCADE
) ENGINE=InnoDB;


