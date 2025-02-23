import React, { useEffect, useState } from "react";
import BookingCard from "../../components/BookingComponents/BookingCard";
import BookingFilter from "../../components/BookingComponents/BookingFilter";
import NavBar from "../../components/NavBar/NavBar";
import { cancelBooking, getBookings } from "../../services/BookingService";

const BookingsPage = () => {
	const [bookings, setBookings] = useState([]);
	const [filters, setFilters] = useState({});
	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState(null);

	useEffect(() => {
		const fetchBookings = async () => {
			setIsLoading(true);
			setError(null);
			try {
				const data = await getBookings(filters);
				setBookings(data);
			} catch (err) {
				setError("Failed to fetch bookings. Please try again later.");
				console.error("Error fetching bookings:", err);
			} finally {
				setIsLoading(false);
			}
		};
		fetchBookings();
	}, [filters]);

	const handleFilterChange = (newFilters) => {
		setFilters(newFilters);
	};

	const handleCancelBooking = async (id) => {
		try {
			await cancelBooking(id);
			const updatedBookings = bookings.filter((booking) => booking.id !== id);
			setBookings(updatedBookings);
		} catch (err) {
			setError("Failed to cancel booking. Please try again.");
			console.error("Error canceling booking:", err);
		}
	};

	if (isLoading) return <div>Loading bookings...</div>;
	if (error) return <div className="error-message">{error}</div>;

	return (
		<div className="bookings-page">
			<NavBar />
			<h1>Bookings</h1>
			<BookingFilter onFilterChange={handleFilterChange} />
			<div className="bookings-list">
				{bookings.length > 0 ? (
					bookings.map((booking) => (
						<BookingCard
							key={booking.id}
							booking={booking}
							onCancelBooking={handleCancelBooking}
						/>
					))
				) : (
					<p>No bookings found.</p>
				)}
			</div>
		</div>
	);
};

export default BookingsPage;
