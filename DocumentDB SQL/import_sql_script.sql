SELECT  TOP (150)
store.Name AS [name] ,
       GETDATE() AS [modified_date] ,
       GETDATE() AS [created_date] ,
       Person.SalesQuota AS [person.sales_qouta] ,
       Person.Bonus AS [person.bonus] ,
       Person.CommissionPct AS [person.commission_pct] ,
       Person.SalesYTD AS [person.sales_ytd] ,
       Person.SalesLastYear AS [person.sales_last_year] ,
       territory.Name AS [person.territory.name] ,
       territory.CountryRegionCode AS [person.territory.country_region_code] ,
       territory.[Group] AS [person.territory.group] ,
       territory.SalesYTD AS [person.territory.sales_ytd] ,
       territory.SalesLastYear AS [person.territory.sales_last_year] ,
       territory.CostYTD AS [person.territory.cost_ytd]
FROM   Sales.Store AS store
       INNER JOIN Sales.SalesPerson AS person ON person.BusinessEntityID = store.SalesPersonID
       INNER JOIN Sales.SalesTerritory AS territory ON territory.TerritoryID = person.TerritoryID

ORDER BY store.BusinessEntityID;