/*                
'+-----------------------------------------------------------------------------                
'| Tabela: TICKET              
'| Descrição: Criação da tabela TICKET           
'| Responsável Criação: João Pedro          
'| Data Criação:  08/06/2020       
'| Data Últ. Modificação:           
'| Descrição:                      
'| Responsável:         
'+-----------------------------------------------------------------------------                
*/   

CREATE TABLE TICKET
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	TITLE VARCHAR(256) NOT NULL,
	DESCRIPTION VARCHAR(256),
	DATE_OCURRENCE DATETIME DEFAULT GETDATE() NOT NULL,
	EMAIL VARCHAR(50) NOT NULL,
	PRIORITY INT NOT NULL,
	STATUS INT NOT NULL,
	DATE_CHANGE DATETIME,
	ISACTIVE BIT
)