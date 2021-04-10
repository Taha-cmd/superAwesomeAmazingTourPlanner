CREATE TABLE IF NOT EXISTS "tour" (
  id SERIAL PRIMARY KEY,
  name varchar UNIQUE,
  description text NOT NULL,
  startingArea varchar NOT NULL,
  targetArea varchar NOT NULL,
  distance double precision NOT NULL
);

CREATE TABLE IF NOT EXISTS "log" (
  id SERIAL PRIMARY KEY,
  "date" timestamp NOT NULL,
  tourName varchar NOT NULL,
  report text NOT NULL,
  totalTime double precision NOT NULL,
  rating int NOT NULL CONSTRAINT rating_limits CHECK (rating >= 0 and rating <= 10)
);

ALTER TABLE "log" ADD FOREIGN KEY (tourName) REFERENCES "tour" (name);

