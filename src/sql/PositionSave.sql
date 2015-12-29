SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

DECLARE @Id int
DECLARE @PersonId int
DECLARE @CompanyId int
DECLARE @TitleId int
DECLARE @StartDate date
DECLARE @EndDate date

IF EXISTS (SELECT * FROM [dbo].[Position] WHERE [Id] = @Id)
	BEGIN
		UPDATE dbo.[Position]
		SET
			[CompanyId] = @CompanyId,
			[TitleId] = @TitleId,
			[StartDate] = @StartDate,
			[EndDate] = @EndDate,
			[Modified] = GetDate()
		WHERE [Id] = @Id
		SELECT @Id
	END
ELSE
	BEGIN
		INSERT INTO dbo.[Position] (
			[PersonId],
			[CompanyId],
			[TitleId],
			[StartDate],
			[EndDate])
		VALUES (
			@PersonId,
			@CompanyId,
			@TitleId,
			@StartDate,
			@EndDate)
		SELECT SCOPE_IDENTITY()
	END