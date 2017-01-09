DROP TABLE rdv_reparation;
DROP TABLE facture;
DROP TABLE rendezvous;
DROP TABLE reparation;
DROP TABLE vehicule;
DROP TABLE client;


CREATE TABLE client(
id INTEGER PRIMARY KEY,
nom VARCHAR2(128),
prenom VARCHAR2(128),
telephone1 VARCHAR2(30),
telephone2 VARCHAR2(30),
email VARCHAR2(512) UNIQUE,
adresse VARCHAR2(512),
ville VARCHAR2(512),
codePostal CHAR(5)
);

CREATE TABLE vehicule(
  id INTEGER PRIMARY KEY,
  kilometrage varchar2(10),
  immatriculation VARCHAR2(20) NOT NULL,
  marque VARCHAR2(512),
  modele VARCHAR2(512),
  annee varchar2(10),
  client INTEGER,
  CONSTRAINT fk_vehicule_client FOREIGN KEY (client) REFERENCES   client(id)  
);

CREATE TABLE rendezvous(
  id INTEGER PRIMARY KEY,
  date_RDV DATE NOT NULL,
  duree NUMBER NOT NULL,
  client INTEGER,
  vehicule INTEGER,
  CONSTRAINT fk_rdv_vehicule FOREIGN KEY (vehicule) REFERENCES   vehicule(id),
  CONSTRAINT fk_rdv_client FOREIGN KEY (client) REFERENCES   client(id)
  
);

CREATE TABLE reparation(
  id INTEGER PRIMARY KEY,
  nom VARCHAR2(512) UNIQUE NOT NULL
);

CREATE TABLE rdv_reparation(
  id INTEGER PRIMARY KEY,
  rdv INTEGER,
  reparation INTEGER,
  reference varchar2(100),
  quantite number(10,2),
  prixu number(10,2),
  remise number(4,2),
  comments VARCHAR2(1024),
  CONSTRAINT fk_rdvtravaux_rdv FOREIGN KEY (rdv) REFERENCES   rendezvous(id),
  CONSTRAINT fk_rdvtravaux_reparation FOREIGN KEY (reparation) REFERENCES   reparation(id)
  
);

CREATE TABLE FACTURE 
   (	ID NUMBER NOT NULL ENABLE, 
	TOTALPIECEHT NUMBER(10,2), 
	REGLEMENT VARCHAR2(20 BYTE), 
	MO1 NUMBER(10,2), 
	MO2 NUMBER(10,2), 
	MO3 NUMBER(10,2), 
	MO4 NUMBER(10,2), 
	MO5 NUMBER(10,2), 
	TOTALHT NUMBER(10,2), 
	RDV NUMBER(38,0), 
	 CONSTRAINT FACTURE_PK PRIMARY KEY (ID), 
	 CONSTRAINT FK_FACT_RDV FOREIGN KEY (RDV)
	  REFERENCES RENDEZVOUS (ID) ENABLE
);


INSERT INTO reparation(id,nom) VALUES(241,'CONTROLE TECHNIQUE');
INSERT INTO reparation(id,nom) VALUES(242,'EMBRAYAGE');
INSERT INTO reparation(id,nom) VALUES(243,'KIT DISTRIBUTION');
INSERT INTO reparation(id,nom) VALUES(244,'POMPE A EAU COURROIE');
INSERT INTO reparation(id,nom) VALUES(245,'BATTERIE');
INSERT INTO reparation(id,nom) VALUES(246,'ECHAPPEMENT');
INSERT INTO reparation(id,nom) VALUES(247,'CLIMATISATION');
INSERT INTO reparation(id,nom) VALUES(248,'DIAGNOSTIC');
INSERT INTO reparation(id,nom) VALUES(249,'AMORTISSEURS');
INSERT INTO reparation(id,nom) VALUES(250,'FREINS');
INSERT INTO reparation(id,nom) VALUES(251,'PNEUS');
INSERT INTO reparation(id,nom) VALUES(252,'HUILE');
INSERT INTO reparation(id,nom) VALUES(253,'FILTRE');
INSERT INTO reparation(id,nom) VALUES(254,'DIVERS');

DROP SEQUENCE seq_facture;
DROP SEQUENCE seq_vehicule;
DROP SEQUENCE seq_client;
DROP SEQUENCE seq_reparation;
DROP SEQUENCE seq_rendezvous;
DROP SEQUENCE seq_rdv_reparation;


CREATE OR REPLACE procedure createSequences
IS
  v_sql VARCHAR(512);
BEGIN
  
  SELECT 'CREATE SEQUENCE seq_vehicule MINVALUE 1 MAXVALUE 999999999 INCREMENT BY 1 START WITH '|| to_number(NVL(MAX(id),0)+1) ||' CACHE 20 ORDER  CYCLE'
    INTO v_sql
    FROM vehicule;
  EXECUTE IMMEDIATE v_sql;

SELECT 'CREATE SEQUENCE seq_client MINVALUE 1 MAXVALUE 999999999 INCREMENT BY 1 START WITH '|| to_number(NVL(MAX(id),0)+1) ||' CACHE 20 ORDER  CYCLE'
    INTO v_sql
    FROM client;
  EXECUTE IMMEDIATE v_sql;
  
  SELECT 'CREATE SEQUENCE seq_rendezvous MINVALUE 1 MAXVALUE 999999999 INCREMENT BY 1 START WITH '|| to_number(NVL(MAX(id),0)+1) ||' CACHE 20 ORDER  CYCLE'
    INTO v_sql
    FROM rendezvous;
  EXECUTE IMMEDIATE v_sql;

SELECT 'CREATE SEQUENCE seq_reparation MINVALUE 1 MAXVALUE 999999999 INCREMENT BY 1 START WITH '|| to_number(NVL(MAX(id),0)+1) ||' CACHE 20 ORDER  CYCLE'
    INTO v_sql
    FROM reparation;
  EXECUTE IMMEDIATE v_sql;
  
  SELECT 'CREATE SEQUENCE seq_rdv_reparation MINVALUE 1 MAXVALUE 999999999 INCREMENT BY 1 START WITH '|| to_number(NVL(MAX(id),0)+1) ||' CACHE 20 ORDER  CYCLE'
    INTO v_sql
    FROM rdv_reparation;
  EXECUTE IMMEDIATE v_sql;

SELECT 'CREATE SEQUENCE seq_facture MINVALUE 1 MAXVALUE 999999999 INCREMENT BY 1 START WITH '|| to_number(NVL(MAX(id),0)+1) ||' CACHE 20 ORDER  CYCLE'
    INTO v_sql
    FROM facture;
  EXECUTE IMMEDIATE v_sql;

END createSequences;
/

EXEC createsequences;

create or replace 
trigger tr_vehicule
BEFORE INSERT ON vehicule
FOR EACH ROW
BEGIN
    IF( :new.id IS NULL )
    THEN
      SELECT seq_vehicule.nextval
      INTO :new.id FROM dual;
    END IF;
END tr_vehicule;
/

create or replace 
trigger tr_client
BEFORE INSERT ON client
FOR EACH ROW
BEGIN
    IF( :new.id IS NULL )
    THEN
      SELECT seq_client.nextval
      INTO :new.id FROM dual;
    END IF;
END tr_client;
/

create or replace 
trigger tr_rendezvous
BEFORE INSERT ON rendezvous
FOR EACH ROW
BEGIN
    IF( :new.id IS NULL )
    THEN
      SELECT seq_rendezvous.nextval
      INTO :new.id FROM dual;
    END IF;
END tr_rendezvous;
/

create or replace 
trigger tr_reparation
BEFORE INSERT ON reparation
FOR EACH ROW
BEGIN
    IF( :new.id IS NULL )
    THEN
        SELECT seq_reparation.nextval
        INTO :new.id FROM dual;
    END IF;
END tr_reparation;
/

create or replace 
trigger tr_rdv_reparation
BEFORE INSERT ON rdv_reparation
FOR EACH ROW
BEGIN
    IF( :new.id IS NULL )
    THEN
      SELECT seq_rdv_reparation.nextval
      INTO :new.id FROM dual;
    END IF;
END tr_rdv_reparation;
/

create or replace trigger tr_facture
BEFORE INSERT ON facture
FOR EACH ROW
BEGIN
    IF( :new.id IS NULL )
    THEN
      SELECT seq_facture.nextval
      INTO :new.id FROM dual;
    END IF;
END tr_facture;
/

commit;