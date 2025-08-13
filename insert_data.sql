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

INSERT INTO public.rentals (customer_name, customer_phone_number, vehicle_registration_number, vehicle_model, pickup_date, return_date, daily_rate, status) VALUES
    ('John Doe', '1234567890', 'ABC123', 'Toyota Corolla', '2023-08-01', '2023-08-10', 29.99, 'Booked'),
    ('Jane Smith', '9876543210', 'XYZ456', 'Honda Civic', '2023-08-03', '2023-08-15', 34.99, 'Booked'),
    ('Bob Johnson', '4567891230', 'LMN789', 'Ford Focus', '2023-08-05', '2023-08-12', 25.50, 'Booked'),
    ('Alice Davis', '1122334455', 'PQR012', 'Toyota Corolla', '2023-08-06', '2023-08-14', 29.99, 'Booked'),
    ('Charlie Brown', '5566778899', 'STU345', 'Honda Civic', '2023-08-07', '2023-08-20', 32.50, 'Booked'),
    ('Debbie Wilson', '6677889900', 'VWX678', 'Ford Focus', '2023-08-02', '2023-08-09', 26.00, 'Booked'),
    ('Eva Martinez', '4455667788', 'DEF890', 'Hyundai Elantra', '2023-08-01', '2023-08-05', 30.00, 'Booked'),
    ('Frank Harris', '3344556677', 'GHI234', 'Toyota Corolla', '2023-08-04', '2023-08-13', 29.00, 'Booked'),
    ('Grace Lewis', '9988776655', 'JKL567', 'Hyundai Elantra', '2023-08-02', '2023-08-11', 33.50, 'Booked'),
    ('Henry Walker', '2233445566', 'MNO890', 'Ford Focus', '2023-08-08', '2023-08-18', 28.50, 'Booked');

