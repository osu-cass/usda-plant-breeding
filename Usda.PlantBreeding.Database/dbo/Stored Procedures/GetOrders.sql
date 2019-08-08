-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetOrders] 
-- paramters for the stored procedure
	@OrderId int,
	@Genus int,
	@Year int,
	@Grower int,
	@Genotype int

AS
BEGIN
	SELECT GivenName, VirusTested, Quantity, m.Name, op.Note
	From OrderProducts op
	Inner Join Orders o on (ISNULL(@OrderId, o.Id) = op.OrderId)
	Left Join Materials m on op.MaterialId = m.Id
	Left Join Genotypes g on (ISNULL(@Genotype, op.GenotypeId) = g.Id)
	Left Join Growers gr on (ISNULL(@Grower, o.GrowerId) = gr.Id)
	Left Join Genus ge on (ISNULL(@Genus, o.GenusId) = ge.Id)
END