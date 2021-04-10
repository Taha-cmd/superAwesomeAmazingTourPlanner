  
-- Role: "TourPlannerAdmin"
-- DROP ROLE "TourPlannerAdmin"


CREATE USER "TourPlannerAdmin" with createdb login password '123';

CREATE DATABASE "TourPlanner" OWNER "TourPlannerAdmin" encoding 'utf8';
GRANT ALL PRIVILEGES ON DATABASE "TourPlanner" TO "TourPlannerAdmin";
--GRANT CONNECT ON "TourPlanner" TO "TourPlannerAdmin";

