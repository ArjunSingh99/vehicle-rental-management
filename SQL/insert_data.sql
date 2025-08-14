-- Insert Customers (10)
INSERT INTO public.customer (name, phone_number) VALUES
    ('John Doe', '1234567890'),
    ('Jane Smith', '9876543210'),
    ('Bob Johnson', '4567891230'),
    ('Alice Davis', '1122334455'),
    ('Charlie Brown', '5566778899'),
    ('Debbie Wilson', '6677889900'),
    ('Eva Martinez', '4455667788'),
    ('Frank Harris', '3344556677'),
    ('Grace Lewis', '9988776655'),
    ('Henry Walker', '2233445566');

INSERT INTO public.vehicle (registration_number, model) VALUES
    ('ABC123', 'Toyota Corolla'),
    ('XYZ456', 'Honda Civic'),
    ('LMN789', 'Ford Focus'),
    ('PQR012', 'Toyota Corolla'),
    ('STU345', 'Honda Civic'),
    ('VWX678', 'Ford Focus'),
    ('DEF890', 'Hyundai Elantra'),
    ('GHI234', 'Toyota Corolla'),
    ('JKL567', 'Hyundai Elantra'),
    ('MNO890', 'Ford Focus');

INSERT INTO public.rentals (
    customer_name,
    customer_phone_number,
    vehicle_registration_number,
    vehicle_model,
    pickup_date,
    return_date,
    daily_rate,
    status
) VALUES
    ('John Doe', '1234567890', 'ABC123', 'Toyota Corolla', '2025-08-24', '2025-09-02', 29.99, 'Booked'),
    ('Jane Smith', '9876543210', 'XYZ456', 'Honda Civic', '2025-08-26', '2025-09-07', 34.99, 'Booked'),
    ('Bob Johnson', '4567891230', 'LMN789', 'Ford Focus', '2025-08-28', '2025-09-04', 25.50, 'Booked'),
    ('Alice Davis', '1122334455', 'PQR012', 'Toyota Corolla', '2025-08-29', '2025-09-06', 29.99, 'Booked'),
    ('Charlie Brown', '5566778899', 'STU345', 'Honda Civic', '2025-08-30', '2025-09-12', 32.50, 'Booked'),
    ('Debbie Wilson', '6677889900', 'VWX678', 'Ford Focus', '2025-08-25', '2025-09-01', 26.00, 'Booked'),
    ('Eva Martinez', '4455667788', 'DEF890', 'Hyundai Elantra', '2025-08-24', '2025-08-28', 30.00, 'Booked'),
    ('Frank Harris', '3344556677', 'GHI234', 'Toyota Corolla', '2025-08-27', '2025-09-05', 29.00, 'Booked'),
    ('Grace Lewis', '9988776655', 'JKL567', 'Hyundai Elantra', '2025-08-25', '2025-09-03', 33.50, 'Booked'),
    ('Henry Walker', '2233445566', 'MNO890', 'Ford Focus', '2025-08-31', '2025-09-10', 28.50, 'Booked');

-- Insert vehicle coordinates
DO $$
DECLARE
    booking RECORD;
    latitude NUMERIC(9,6);
    longitude NUMERIC(9,6);
    base_time TIMESTAMP;
    i INT;
BEGIN
    -- Loop through each booking in the rentals table
    FOR booking IN SELECT id, pickup_date, return_date FROM public.rentals LOOP
        -- Start base time at pickup_date + random hour offset (e.g., 0–8 AM)
        base_time := booking.pickup_date + (FLOOR(random() * 8) || ' hours')::INTERVAL;

        -- Insert 10 locations at 10-minute intervals
        FOR i IN 0..9 LOOP -- 10 iterations for 10 minutes
            latitude := 40 + random() * 10;
            longitude := -75 - random() * 10;

            INSERT INTO public.vehicle_location (
                booking_id,
                latitude,
                longitude,
                "timestamp"
            ) VALUES (
                booking.id,
                latitude,
                longitude,
                base_time + (i * interval '10 minute')
            );
        END LOOP;
    END LOOP;
END $$;
