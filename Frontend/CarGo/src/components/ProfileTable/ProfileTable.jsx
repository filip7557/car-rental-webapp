import './ProfileTable.css'

function ProfileTable({ user }) {
    return (
        <div className="profileTable">
            <table>
                <tbody>
                    <tr><td>Fullname:</td><td>{user.fullName}</td></tr>
                    <tr><td>Phonenumber:</td><td>{user.phoneNumber}</td></tr>
                    <tr><td>Role:</td><td>{user.role}</td></tr>
                    <tr><td>Email:</td><td>{user.email}</td></tr>
                </tbody>
            </table>
        </div>
    )
}

export default ProfileTable