/*                
'+-----------------------------------------------------------------------------                
'| Tabela: TICKET              
'| Descri��o: Cria��o da tabela TICKET           
'| Respons�vel Cria��o: Jo�o Pedro          
'| Data Cria��o:  08/06/2020       
'| Data �lt. Modifica��o:           
'| Descri��o:                      
'| Respons�vel:         
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