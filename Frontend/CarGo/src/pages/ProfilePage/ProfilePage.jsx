import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import userService from '../../services/UserService';
import './ProfilePage.css'

import NavBar from '../../components/NavBar/NavBar';
import ProfileTable from '../../components/ProfileTable/ProfileTable';

function ProfilePage() {
    
    const [user, setUser] = useState({});
    const navigate = useNavigate();

    useEffect(() => {
        const userId = localStorage.getItem("userId")
        if (userId) {
        userService.getUser(userId)
            .then(setUser);
        }
        else {
            navigate("/login");
        }
    }, [])

    function handleClick() {
        // TODO: Navigatet to edit profile page.
    }
    
    return (
        <div>
            <NavBar />
            <div className='profilePage'>
                <ProfileTable user={user} />
                <button onClick={handleClick}>Edit profile</button>
            </div>
        </div>
    )
}

export default ProfilePage;