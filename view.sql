SELECT * FROM restaurantdb.customer;

create view restaurantdb.restaurantview as
select c.Id as Id, r.Name as RestaurantName, r.Id as RestaurantId, rm.MealName as MealName,
sum(rm.PriceInNis) as ProfitInNis,
rm.Id as RestaurantMenuId,
sum(rm.PriceInUsd) as ProfitInUsd,
count(DISTINCT c.Id) as NumberOfOrderCustomer

from restaurantdb.order o
join restaurantdb.customer c on o.CustomerId = c.Id
join restaurantdb.restaurantmenu rm on rm.Id = o.ResturantMenuId
join restaurantdb.restaurant r on r.Id = rm.restaurantId
group by RestaurantId;

