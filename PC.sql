--Populating Medic

INSERT INTO [dbo].[Medic]
VALUES ('Anghel', 'Negoita', 'anghel_negoita');

--Populating Assistant

INSERT INTO [dbo].[Assistant]
VALUES ('Olimpia', 'Bradatan', 1, 'oli_brada');

--Populating Admin

INSERT INTO [dbo].[Admin]
VALUES ('adminadmin',null);

--Populating medicalPrescription

INSERT INTO [dbo].[medicalPrescription]
VALUES ('Pneumonie', 'Antibotic', 0);

INSERT INTO [dbo].[medicalPrescription]
VALUES ('Gripa cu tuse seaca', 'Paracetamol 2/zi + sirop Eurespal', 1);

INSERT INTO [dbo].[medicalPrescription]
VALUES ('Imunitate scazuta', 'Vitamine + Cetebe', 0);

INSERT INTO [dbo].[medicalPrescription]
VALUES ('Alergie la praf', 'Aerius 1/zi', 1);

INSERT INTO [dbo].[medicalPrescription]
VALUES ('Febra si dureri musculare', 'Masaj Fastum Gel 3/zi', 0);

INSERT INTO [dbo].[medicalPrescription]
VALUES ('Herpes', 'SystemWell 2/zi', 0);

--Populating medicalRecord

INSERT INTO [dbo].[medicalRecord]
VALUES ('2015-04-04', 'Hepatita, Rujeola', 'Hipertensiune arteriala', 'Rujeola, Oreon', 'Aspacardin 1/zi', 'lipsa', '2016-03-09', 'lipsa');

INSERT INTO [dbo].[medicalRecord]
VALUES ('2017-11-12', 'Varicela, Rujeola', 'Hipertensiune arteriala', 'Rujeola', 'lipsa', 'lipsa', '2018-01-05', 'lipsa');

INSERT INTO [dbo].[medicalRecord]
VALUES ('2015-04-04', 'Hepatita, Rujeola', 'Hipertensiune arteriala', 'Rujeola, Oreon', 'Aspacardin 1/zi', 'lipsa', '2016-03-09', 'lipsa');

INSERT INTO [dbo].[medicalRecord]
VALUES ('2017-11-12', 'Varicela, Rujeola', 'Hipertensiune arteriala', 'Rujeola', 'lipsa', 'lipsa', '2018-01-05', 'lipsa');

INSERT INTO [dbo].[medicalRecord]
VALUES ('2017-12-12', 'Varicela, Rujeola', 'Hipertensiune arteriala', 'Rujeola', 'lipsa', 'lipsa', '2018-01-05', 'lipsa');

INSERT INTO [dbo].[medicalRecord]
VALUES ('2017-11-11', 'Varicela, Rujeola', 'Hipertensiune arteriala', 'Rujeola', 'lipsa', 'lipsa', '2018-01-05', 'lipsa');

--Populating Patient

INSERT INTO [dbo].[Patient]
VALUES ('28584970695048327384', 'Cipri', 'Nistor', '1992-08-22', 'cipri@gmail.com', 1, '1920822342673', 1);

INSERT INTO [dbo].[Patient]
VALUES ('18582742104908327384', 'Iulia', 'Albu', '1983-11-21', 'aiulia@gmail.com', 1, '1920822342673', 2);

INSERT INTO [dbo].[Patient]
VALUES ('84724949380048382734', 'Alexandra', 'Costea', '1998-03-01', 'calexandra@gmail.com', 1, '1920822342673', 3);

INSERT INTO [dbo].[Patient]
VALUES ('95048327384285849706', 'Ahmad', 'Bayan', '1995-11-12', 'noaa322@gmail.com', 1, '1951112160098', 4);

INSERT INTO [dbo].[Patient]
VALUES ('95085828374657289706', 'Marian-Valer', 'Bolintineanu', '1997-06-05', 'bolintineanuvaler@gmail.com', 1, '1970605303722', 5);

INSERT INTO [dbo].[Patient]
VALUES ('95085828373657289706', 'Robert', 'Corman', '1996-09-08', 'robert.corman89@gmail.com', 1, '1960908071970', 6);


--Populating Appointment

INSERT INTO [dbo].[Appointment]
VALUES (1,'28584970695048327384', '2008-09-08', '16:00:00', 4);

INSERT INTO [dbo].[Appointment]
VALUES (1,'18582742104908327384', '2018-09-08', '12:00:00', 2);

INSERT INTO [dbo].[Appointment]
VALUES (1,'84724949380048382734', '2018-04-08', '09:30:00', 5);

INSERT INTO [dbo].[Appointment]
VALUES (1,'95048327384285849706', '2018-04-04', '15:30:00', 1);

INSERT INTO [dbo].[Appointment]
VALUES (1,'95085828374657289706', '2018-07-04', '11:45:00', 3);

--Populating AspNetRole

INSERT INTO [dbo].[AspNetRole]
VALUES('Admin');

INSERT INTO [dbo].[AspNetRole]
VALUES('Assistant');

INSERT INTO [dbo].[AspNetRole]
VALUES('Medic');

INSERT INTO [dbo].[AspNetRole]
VALUES('Patient');

--Populating AspNetUser

INSERT INTO [dbo].[AspNetUser]
VALUES ('medic@gmail.com', null, 'cdimBKgXwKi01+gUyvdbDg==', null, null, null, null, null, null, null, null, 3, null, null, null, 84724949380048382734, null  );

INSERT INTO [dbo].[AspNetUser]
VALUES ('asisstant@gmail.com', null, 'cdimBKgXwKi01+gUyvdbDg==', null, null, null, null, null, null, null, null, 2, null, null, null, 84724949380048382734, null  );

INSERT INTO [dbo].[AspNetUser]
VALUES ('admin@gmail.com', null, 'cdimBKgXwKi01+gUyvdbDg==', null, null, null, null, null, null, null, null, 1, null, null, null, 84724949380048382734, null  );
