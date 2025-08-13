-- NOTE: This SQL script is designed to create tables in a PostgreSQL database

-- Create Customer Table
CREATE TABLE IF NOT EXISTS public.customer
(
    id SERIAL PRIMARY KEY,                 -- Auto-incrementing ID
    name VARCHAR(255) NOT NULL,             -- Customer name
    phone_number CHAR(10) NOT NULL,        -- Customer phone number
    CONSTRAINT unique_customer_name_phone UNIQUE (name, phone_number)  -- Unique constraint on name and phone
);

-- Create Vehicle Table
CREATE TABLE IF NOT EXISTS public.vehicle
(
    id SERIAL PRIMARY KEY,                 -- Auto-incrementing ID
    model VARCHAR(255) NOT NULL,            -- Vehicle model
    registration_number VARCHAR(255) NOT NULL, -- Vehicle registration number
    CONSTRAINT unique_reg_no UNIQUE (registration_number),  -- Unique constraint on registration number
    CONSTRAINT unique_vehicle_registration_model UNIQUE (registration_number, model)  -- Unique constraint on both reg no and model
);

-- Create Rentals Table
CREATE TABLE IF NOT EXISTS public.rentals
(
    id SERIAL PRIMARY KEY,                 -- Auto-incrementing ID
    customer_name VARCHAR(255) NOT NULL,    -- Customer name
    customer_phone_number CHAR(10) NOT NULL, -- Customer phone number
    vehicle_registration_number VARCHAR(255) NOT NULL, -- Vehicle registration number
    vehicle_model VARCHAR(255) NOT NULL,    -- Vehicle model
    pickup_date DATE NOT NULL,             -- Pickup date
    return_date DATE NOT NULL,             -- Return date
    daily_rate DOUBLE PRECISION NOT NULL,  -- Daily rate
    status VARCHAR(10) DEFAULT 'Booked',   -- Booking status
    CONSTRAINT rentals_customer_fkey FOREIGN KEY (customer_name, customer_phone_number)
        REFERENCES public.customer (name, phone_number),
    CONSTRAINT rentals_vehicle_fkey FOREIGN KEY (vehicle_registration_number, vehicle_model)
        REFERENCES public.vehicle (registration_number, model)
);

-- Create Vehicle Location Table
CREATE TABLE IF NOT EXISTS public.vehicle_location
(
    id SERIAL PRIMARY KEY,                -- Auto-incrementing ID
    booking_id INTEGER,                   -- Booking ID (foreign key)
    latitude NUMERIC(9, 6),               -- Latitude (with 6 decimal places)
    longitude NUMERIC(9, 6),              -- Longitude (with 6 decimal places)
    "timestamp" TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Timestamp for location record
    CONSTRAINT vehicle_location_booking_id_fkey FOREIGN KEY (booking_id)
        REFERENCES public.rentals (id)
);
