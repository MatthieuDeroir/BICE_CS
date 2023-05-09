DROP DATABASE BICE_DATABASE;
CREATE DATABASE BICE_DATABASE;
GO

USE BICE_DATABASE;
GO

-- Table pour stocker les informations sur les véhicules
CREATE TABLE Vehicles (
    id INT PRIMARY KEY IDENTITY(1,1),
    internalNumber NVARCHAR(50) NOT NULL UNIQUE,
    denomination NVARCHAR(255) NOT NULL,
    licensePlate NVARCHAR(20) NOT NULL UNIQUE,
    isActive BIT NOT NULL DEFAULT 1
);

-- Table pour stocker les informations sur le matériel
CREATE TABLE Materials (
    id INT PRIMARY KEY IDENTITY(1,1),
    barcode NVARCHAR(50) NOT NULL UNIQUE,
    denomination NVARCHAR(255) NOT NULL,
    category NVARCHAR(255) NOT NULL,
    usageCount INT NOT NULL DEFAULT 0,
    maxUsageCount INT NULL,
    expirationDate DATETIME NULL,
    nextControlDate DATETIME NULL,
    isStored BIT NOT NULL DEFAULT 1,
    isLost BIT NOT NULL DEFAULT 0,
    id_vehicle INT NULL,
    FOREIGN KEY (id_vehicle) REFERENCES Vehicles(id)
);

-- Table pour stocker les informations sur les interventions
CREATE TABLE Interventions (
    id INT PRIMARY KEY IDENTITY(1,1),
    denomination NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX) NULL,
    startDate DATETIME NOT NULL,
    endDate DATETIME NULL,
);

-- Table pour stocker les vehicules par intervention
CREATE TABLE VehicleIntervention (
    id_vehicle INT NOT NULL,
    id_intervention INT NOT NULL,
    FOREIGN KEY (id_intervention) REFERENCES Interventions(id),
    FOREIGN KEY (id_vehicle) REFERENCES Vehicles(id)
);

-- Table pour stocker l'historique d'utilisation du matériel
CREATE TABLE MaterialUsageHistory (
    id INT PRIMARY KEY IDENTITY(1,1),
    id_material INT NOT NULL,
    id_vehicle_intervention INT NOT NULL,
    usage_date DATETIME NOT NULL,
    is_used BIT NOT NULL DEFAULT 1,
    is_lost BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (id_material) REFERENCES Materials(id),
    FOREIGN KEY (id_vehicle_intervention) REFERENCES VehicleIntervention(id)
);
