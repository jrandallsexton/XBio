SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

DECLARE @Id int
DECLARE @PositionId int
DECLARE @Title varchar(50)
DECLARE @Value varchar(max)
DECLARE @Order int

IF EXISTS (SELECT * FROM [dbo].[PositionDetail] WHERE [Id] = @Id)
	BEGIN
		UPDATE dbo.[PositionDetail]
		SET
			[PositionId] = @PositionId,
			[Title] = @Title,
			[Value] = @Value,
			[Order] = @Order,
			[Modified] = GetDate()
		WHERE [Id] = @Id
		SELECT @Id
	END
ELSE
	BEGIN
		INSERT INTO dbo.[PositionDetail] (
			[PositionId],
			[Title],
			[Value],
			[Order])
		VALUES (
			@PositionId,
			@Title,
			@Value,
			@Order)
		SELECT SCOPE_IDENTITY()
	END