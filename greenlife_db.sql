CREATE DATABASE `greenlife` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

-- greenlife.admins definition

CREATE TABLE `admins` (
  `admin_id` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL,
  `password` varchar(64) NOT NULL,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`admin_id`),
  UNIQUE KEY `username` (`username`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO greenlife.admins (username,password,created_at) VALUES
	 ('admin','8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918','2026-01-18 07:15:57');

-- greenlife.customers definition

CREATE TABLE `customers` (
  `customer_id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(64) NOT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `address` text,
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `modified_date` datetime DEFAULT NULL,
  `active_status` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`customer_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- greenlife.products definition

CREATE TABLE `products` (
  `product_id` int NOT NULL AUTO_INCREMENT,
  `product_name` varchar(100) NOT NULL,
  `category` varchar(50) DEFAULT NULL,
  `price` decimal(10,2) NOT NULL,
  `stock_quantity` int NOT NULL,
  `supplier` varchar(100) DEFAULT NULL,
  `discount` decimal(5,2) DEFAULT '0.00',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  `modified_date` datetime DEFAULT NULL,
  `active_status` tinyint(1) NOT NULL DEFAULT '1',
  PRIMARY KEY (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

INSERT INTO greenlife.products (product_name,category,price,stock_quantity,supplier,discount,created_at,modified_date,active_status) VALUES
	 ('Rice','Grains',300.00,48,'GreenFarm',0.00,'2026-01-18 07:20:57','2026-01-31 23:11:44',1),
	 ('Honey','Sweeteners',1500.00,27,'BeePure',0.00,'2026-01-18 07:20:57','2026-01-31 23:11:44',1),
	 ('Coconut Oil','Oils',500.00,22,'CocoPure',0.00,'2026-01-18 07:20:57','2026-01-31 23:11:44',1),
	 ('Green Tea','Beverages',600.00,40,'NatureLeaf',0.00,'2026-01-18 07:20:57',NULL,1),
	 ('Brown Sugar','Sweeteners',400.00,59,'SugarCane Co',0.00,'2026-01-18 07:20:57','2026-01-31 23:11:51',1),
	 ('Lentils','Pulses',450.00,45,'PulseFarm',0.00,'2026-01-18 07:20:57',NULL,1),
	 ('Turmeric Powder','Spices',1250.00,33,'SpiceRoot',0.00,'2026-01-18 07:20:57','2026-01-31 23:11:51',1),
	 ('Black Pepper','Spices',900.00,20,'SpiceRoot',0.00,'2026-01-18 07:20:57',NULL,1),
	 ('Cinnamon','Spices',350.00,28,'SpiceRoot',0.00,'2026-01-18 07:20:57',NULL,1),
	 ('Almonds','Nuts',1750.00,15,'NutHarvest',0.00,'2026-01-18 07:20:57',NULL,1);

-- greenlife.orders definition

CREATE TABLE `orders` (
  `order_id` int NOT NULL AUTO_INCREMENT,
  `customer_id` int NOT NULL,
  `order_date` datetime DEFAULT CURRENT_TIMESTAMP,
  `total_amount` decimal(10,2) NOT NULL,
  `order_status` enum('Pending','Confirmed','Shipped','Delivered','Cancelled') NOT NULL DEFAULT 'Pending',
  PRIMARY KEY (`order_id`),
  KEY `customer_id` (`customer_id`),
  CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- greenlife.order_items definition

CREATE TABLE `order_items` (
  `order_item_id` int NOT NULL AUTO_INCREMENT,
  `order_id` int NOT NULL,
  `product_id` int NOT NULL,
  `quantity` int NOT NULL,
  `unit_price` decimal(10,2) NOT NULL,
  `discount` int DEFAULT '0',
  PRIMARY KEY (`order_item_id`),
  KEY `order_id` (`order_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `order_items_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`),
  CONSTRAINT `order_items_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- greenlife.reviews definition

CREATE TABLE `reviews` (
  `review_id` int NOT NULL AUTO_INCREMENT,
  `product_id` int NOT NULL,
  `customer_id` int NOT NULL,
  `rating` int DEFAULT NULL,
  `comment` text,
  `review_date` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`review_id`),
  KEY `product_id` (`product_id`),
  KEY `customer_id` (`customer_id`),
  CONSTRAINT `reviews_ibfk_1` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`),
  CONSTRAINT `reviews_ibfk_2` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`),
  CONSTRAINT `reviews_chk_1` CHECK ((`rating` between 1 and 5))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;