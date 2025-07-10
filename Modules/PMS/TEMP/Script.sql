USE [RPLEMANAGER] 
GO 
BEGIN TRY 
    BEGIN TRAN 
    EXEC DBO.[JobCorrection] 'PRG',1246,'Auto','','22-Sep-2023',61210,'02-Nov-2023',61960,'S' 
    EXEC DBO.[sp_Insert_Communication_Export] PRG,'Job-Correction',1,'PRG-1246','AUTO'
    COMMIT
END TRY 
BEGIN CATCH 
    ROLLBACK 
END CATCH 
GO 