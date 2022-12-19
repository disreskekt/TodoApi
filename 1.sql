SELECT "ClientName", COUNT(*)
FROM "Clients"
LEFT JOIN "ClientContacts" ON "Clients"."Id" = "ClientId"
GROUP BY "ClientName"