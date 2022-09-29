SELECT * FROM restaurantdb.customer;

create view restaurantdb.restaurantview as
select c.Id as Id, r.Name as RestaurantName, r.Id as RestaurantId, rm.MealName as MealName,
sum(rm.PriceInNis) as ProfitInNis,
rm.Id as RestaurantMenuId,
sum(rm.PriceInUsd) as ProfitInUsd,
count(DISTINCT c.Id) as NumberOfOrderCustomer
from restaurantdb.order o
join restaurantdb.customer c on o.CustomerId = c.Id
join restaurantdb.restaurantmenu rm on rm.Id = o.ResturantMenuId
join restaurantdb.restaurant r on r.Id = rm.restaurantId

group by RestaurantId;CREATE TABLE `customer` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(255) NOT NULL DEFAULT '',
  `LastName` varchar(255) NOT NULL DEFAULT '',
  `CreatedDateUTC` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDateUTC` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Archived` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `order` (
  `ResturantMenuId` int unsigned NOT NULL,
  `CustomerId` int unsigned NOT NULL,
  `RestaurantName` varchar(255) NOT NULL,
  `Id` int NOT NULL,
  `MealName` varchar(255) NOT NULL,
  `PriceInNis` float NOT NULL,
  `PriceInUsd` float NOT NULL,
  `RestaurantmenuId` int NOT NULL,
  `CustomerName` varchar(255) NOT NULL,
  `Price` float NOT NULL,
  PRIMARY KEY (`ResturantMenuId`,`CustomerId`),
  KEY `Order_Customer_idx` (`CustomerId`),
  CONSTRAINT `Order_Customer` FOREIGN KEY (`CustomerId`) REFERENCES `customer` (`Id`),
  CONSTRAINT `Order_ResurantMenue` FOREIGN KEY (`ResturantMenuId`) REFERENCES `restaurantmenu` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `restaurant` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL DEFAULT '',
  `PhoneNumber` varchar(10) NOT NULL DEFAULT '',
  `CreatedDateUTC` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDateUTC` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Archived` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
CREATE TABLE `restaurantmenu` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `MealName` varchar(255) NOT NULL DEFAULT '',
  `PriceInNis` float NOT NULL DEFAULT '10',
  `PriceInUsd` float NOT NULL DEFAULT '10',
  `Quantity` int NOT NULL DEFAULT '10',
  `restaurantId` int unsigned NOT NULL,
  `CreatedDateUTC` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UpdatedDateUTC` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Archived` tinyint NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `restaurantId_UNIQUE` (`restaurantId`),
  CONSTRAINT `resturantmenue_resturant` FOREIGN KEY (`restaurantId`) REFERENCES `restaurant` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
