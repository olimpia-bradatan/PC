INSERT INTO Medic
VALUES ('Popescu', 'Ion', 'ionp')

INSERT INTO Assistant
VALUES ('Ionescu', 'Adela', 1, 'iadela')

INSERT INTO medicalRecord
VALUES(null,null,null,null,null,null,null,null)

INSERT INTO medicalRecord
VALUES ('2009-10-10', 'vaccinari', 'boli', 'bolianterioare', 'medicamente', 'alergii', 'ultimul control', null)

INSERT INTO Patient
VALUES (12345678912345678910, 'Calinescu', 'Andrei', '1990-08-12', 'candrei@yahoo.com', 1, 1900812330198, null)

INSERT INTO Patient
VALUES (12345678912345678900, 'Pop', 'Ioana', '1982-12-20', 'pioana@yahoo.com', 1, 2821220330198, null)

INSERT INTO Patient
VALUES (12345678912345678903, 'Popa', 'Ionel', '1982-12-20', 'pionel@yahoo.com', 1, 2821220330197, 2)

INSERT INTO Patient
VALUES (12345678912345678902, 'Sturzu', 'Ana', '1970-01-01', 'sana@yahoo.com', 1, 1700101330156, null)

INSERT INTO Appointment
VALUES (1, 12345678912345678900, '2018-04-20', '12:00', null)

INSERT INTO Appointment
VALUES (1, 12345678912345678902, '2018-05-28', '10:00', null)

INSERT INTO Appointment
VALUES (1, 12345678912345678900, '2018-04-20', '12:00', null)

INSERT INTO medicalPrescription
VALUES ('pneumonie', 'pastile', 0)

INSERT INTO medicalPrescription
VALUES ('pneumonie2', 'pastile2', 1)

INSERT INTO Appointment
VALUES (1, 12345678912345678910, '2018-04-20', '11:00', 1)