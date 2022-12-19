SELECT "ClientName"
FROM "Clients"
LEFT JOIN "ClientContacts" ON "Clients"."Id" = "ClientId"
GROUP BY "ClientName"
HAVING COUNT(*) > 2