import { useEffect, useState } from "react";
import notificationService from "../../services/NotificationService";
import "./NotificationsPage.css";

import NavBar from "../../components/NavBar/NavBar";
import NotificationCard from "../../components/NotificationCard/NotificationCard";

function NotificationsPage() {
  const [notifications, setNotifications] = useState({data: []});
  const [pages, setPages] = useState([]);
  const [currentPage, setCurrentPage] = useState(
    notificationService.currentPage
  )

  useEffect(() => {
    let numberOfPages = Math.ceil(
      notifications.totalRecords / notifications.pageSize
    );
    let newPages = [];
    for (let i = 1; i <= numberOfPages; i++) {
      newPages.push(i);
    }
    setPages(newPages);
    setCurrentPage(notificationService.currentPage);
  }, [notifications]);

  useEffect(() => {
    notificationService.getNotifications().then(setNotifications);
  }, [])

  function handlePageClick(page) {
    setCurrentPage(page);
    notificationService.currentPage = page;
    notificationService.getNotifications().then(setNotifications);
  }

  return (
    <div>
      <NavBar />
      <div className="notificationsPage">
        <h1>Sent Notifications</h1>
        {notifications.data.map(notification => <NotificationCard key={notification.id} notification={notification} />)}
        {pages.map((page) => (
          <label
            key={page}
            className={page === currentPage ? "currentPage" : ""}
            onClick={() => handlePageClick(page)}
          >
            {page}{" "}
          </label>
        ))}
      </div>
    </div>
  );
}

export default NotificationsPage;
