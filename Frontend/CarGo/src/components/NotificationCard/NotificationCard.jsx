import './NotificationCard.css'

function NotificationCard({ notification }) {
    return (
        <div className='notificationCard'>
            <h5>{notification.dateCreated}</h5>
            <h5>From: {notification.from}</h5>
            <h5>To: {notification.to}</h5>
            <h2>{notification.title}</h2>
            <p>{notification.text}</p>
        </div>
    )
}

export default NotificationCard;