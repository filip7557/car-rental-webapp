import './ManagerCard.css'

function ManagerCard({ manager, isInSearch, addManager, removeManager }) {
    function handleClick() {
        if (isInSearch) {
            addManager(manager);
        }
        else {
            removeManager(manager);
        }
    }
    return (
        <div className='managerCard'>
            <h4>{manager.fullName}</h4>
            <p>{manager.email}</p>
            <p>{manager.phoneNumber}</p>
            <button onClick={handleClick}>{isInSearch ? "Add" : "Remove"}</button>
        </div>
    )
}

export default ManagerCard;