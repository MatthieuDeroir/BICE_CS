DELETE FROM MaterialUsageHistory;
DELETE FROM VehicleIntervention;
DELETE FROM Vehicles;
DELETE FROM Interventions;
DELETE FROM Materials;

USE BICE_DATABASE;
GO

-- Populate Vehicles
INSERT INTO Vehicles(internalNumber, denomination, licensePlate) VALUES ('V001', 'Vehicle 1', 'LP001');
INSERT INTO Vehicles(internalNumber, denomination, licensePlate) VALUES ('V002', 'Vehicle 2', 'LP002');

-- Populate Materials
INSERT INTO Materials(barcode, denomination, category, id_vehicle) VALUES ('BC001', 'Material 1', 'Category 1', 1);
INSERT INTO Materials(barcode, denomination, category, id_vehicle) VALUES ('BC002', 'Material 2', 'Category 1', 1);
INSERT INTO Materials(barcode, denomination, category, id_vehicle) VALUES ('BC003', 'Material 3', 'Category 2', 2);
INSERT INTO Materials(barcode, denomination, category, id_vehicle) VALUES ('BC004', 'Material 4', 'Category 2', 2);

-- Populate Interventions
DECLARE @Now DATETIME;
SET @Now = GETDATE();
INSERT INTO Interventions(denomination, description, startDate) VALUES ('Intervention 1', 'Description 1', @Now);
INSERT INTO Interventions(denomination, description, startDate) VALUES ('Intervention 2', 'Description 2', DATEADD(DAY, -1, @Now));

-- Populate VehicleIntervention
INSERT INTO VehicleIntervention(id_vehicle, id_intervention) VALUES (1, 1);
INSERT INTO VehicleIntervention(id_vehicle, id_intervention) VALUES (2, 2);

-- Populate MaterialUsageHistory
DECLARE @Now DATETIME;
SET @Now = GETDATE();
INSERT INTO MaterialUsageHistory(id_material, id_vehicle_intervention, usage_date) VALUES (1, 1, @Now);
INSERT INTO MaterialUsageHistory(id_material, id_vehicle_intervention, usage_date) VALUES (2, 1, @Now);
INSERT INTO MaterialUsageHistory(id_material, id_vehicle_intervention, usage_date, is_used) VALUES (3, 2, DATEADD(DAY, -1, @Now), 0);

SELECT * FROM MaterialUsageHistory;
SELECT * FROM Materials;
SELECT * FROM Vehicles;
SELECT * FROM Interventions;
SELECT * FROM VehicleIntervention;