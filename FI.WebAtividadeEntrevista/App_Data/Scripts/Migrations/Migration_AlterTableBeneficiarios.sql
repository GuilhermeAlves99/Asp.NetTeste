﻿ALTER TABLE [dbo].[BENEFICIARIOS]
ADD CONSTRAINT FK_Beneficiarios_Clientes
FOREIGN KEY (IDCLIENTE) REFERENCES [dbo].[CLIENTES] (ID);