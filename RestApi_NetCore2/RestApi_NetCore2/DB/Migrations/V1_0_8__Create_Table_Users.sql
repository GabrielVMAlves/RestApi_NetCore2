﻿CREATE TABLE `users` (
	`ID` INT(10) NOT NULL AUTO_INCREMENT,
	`Username` VARCHAR(50) UNIQUE NOT NULL,
	`AccessKey` VARCHAR(50) NOT NULL,
	`Profile` VARCHAR(3) NOT NULL,
	PRIMARY KEY (`ID`)
)
ENGINE=InnoDB;