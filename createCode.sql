-- Table structure for table `cersam`
DROP TABLE IF EXISTS `cersam`;
CREATE TABLE `cersam` (
  `COD` varchar(30) NOT NULL,
  `NAME` varchar(30) DEFAULT (_utf8mb4'cersam'),
  `PASSWORD` varchar(30) DEFAULT (_utf8mb4'senha'),
  `TOKEN` int NOT NULL,
  PRIMARY KEY (`COD`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Table structure for table `donation`
DROP TABLE IF EXISTS `donation`;
CREATE TABLE `donation` (
  `COD` varchar(30) NOT NULL,
  `COD_USER` varchar(30) NOT NULL,
  `COD_PC` varchar(30) DEFAULT NULL,
  `COD_LOT` varchar(30) DEFAULT NULL,
  `DATE` datetime DEFAULT NULL,
  `DESCRIPTION` varchar(255) DEFAULT NULL,
  `FINISHED` tinyint(1) NOT NULL,
  PRIMARY KEY (`COD`),
  KEY `COD_USER` (`COD_USER`),
  KEY `COD_PC` (`COD_PC`),
  KEY `COD_LOT` (`COD_LOT`),
  CONSTRAINT `donation_ibfk_1` FOREIGN KEY (`COD_USER`) REFERENCES `donor` (`COD`) ON DELETE CASCADE,
  CONSTRAINT `donation_ibfk_2` FOREIGN KEY (`COD_PC`) REFERENCES `pcs` (`COD`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Table structure for table `donation_lot`
DROP TABLE IF EXISTS `donation_lot`;
CREATE TABLE `donation_lot` (
  `COD` varchar(30) NOT NULL,
  `COD_PC` varchar(30) DEFAULT NULL,
  `COD_CERSAM` varchar(30) DEFAULT NULL,
  `DATE` datetime NOT NULL,
  PRIMARY KEY (`COD`),
  KEY `COD_CERSAM` (`COD_CERSAM`),
  KEY `COD_PC` (`COD_PC`),
  CONSTRAINT `donation_lot_ibfk_1` FOREIGN KEY (`COD_CERSAM`) REFERENCES `cersam` (`COD`) ON DELETE SET NULL,
  CONSTRAINT `donation_lot_ibfk_2` FOREIGN KEY (`COD_PC`) REFERENCES `pcs` (`COD`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Table structure for table `donor`
DROP TABLE IF EXISTS `donor`;
CREATE TABLE `donor` (
  `COD` varchar(15) NOT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`COD`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Table structure for table `pcs`
DROP TABLE IF EXISTS `pcs`;
CREATE TABLE `pcs` (
  `COD` varchar(30) NOT NULL,
  `COD_USER` varchar(30) DEFAULT NULL,
  `CEP` varchar(10) NOT NULL,
  `STREET` varchar(30) NOT NULL,
  `NUMBER` varchar(10) DEFAULT NULL,
  `NEIGHBORHOOD` varchar(30) NOT NULL,
  `COMPLEMENT` varchar(30) DEFAULT NULL,
  `CITY` varchar(30) NOT NULL,
  `STATE` varchar(30) NOT NULL,
  PRIMARY KEY (`COD`),
  KEY `USER_COD` (`COD_USER`),
  CONSTRAINT `pcs_ibfk_1` FOREIGN KEY (`COD_USER`) REFERENCES `users` (`COD`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Table structure for table `schedule_availability`
DROP TABLE IF EXISTS `schedule_availability`;
CREATE TABLE `schedule_availability` (
  `COD` varchar(15) NOT NULL,
  `DAY` enum('Segunda','Terça','Quarta','Quinta','Sexta','Sábado','Domingo') NOT NULL,
  `BEGIN_TIME` time NOT NULL,
  `END_TIME` time NOT NULL,
  PRIMARY KEY (`COD`),
  CONSTRAINT `FK_AVAIBILITY` FOREIGN KEY (`COD`) REFERENCES `pcs` (`COD`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Table structure for table `users`
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `COD` varchar(50) NOT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `EMAIL` varchar(100) DEFAULT NULL,
  `DDD` varchar(15) DEFAULT NULL,
  `PHONE` varchar(20) DEFAULT NULL,
  `PASSWORD` varchar(255) DEFAULT NULL,
  `IMGBIN` blob,
  PRIMARY KEY (`COD`),
  UNIQUE KEY `UNIQUE_Email` (`EMAIL`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
