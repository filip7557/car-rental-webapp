const CompanyVehicleMaintenanceTable = ({ maintenanceRecords }) => {
	if (!Array.isArray(maintenanceRecords) || maintenanceRecords.length === 0) {
		return <p>Nema podataka za prikaz.</p>;
	}

	return (
		<table>
			<thead>
				<tr>
					<th>Title</th>
					<th>Description</th>
				</tr>
			</thead>
			<tbody>
				{maintenanceRecords.map((item, index) => (
					<tr key={index}>
						<td>{item.title || "N/A"}</td>
						<td>{item.description || "N/A"}</td>
					</tr>
				))}
			</tbody>
		</table>
	);
};

export default CompanyVehicleMaintenanceTable;
