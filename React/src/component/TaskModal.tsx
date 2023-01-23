import { ITask } from "./Task.type";
import "../CSS/TaskModal.style.css";

type Props = {
  onClose: () => void;
  data: ITask;
};

const TaskModal = (props: Props) => {
  const { onClose, data } = props;
  return (
    <div id="myModal" className="modal">
      <div className="modal-content">
        <span className="close" onClick={onClose}>
          &times;
        </span>
        <h3>Tasks</h3>
        <div>
          <div>
            <label>Id : {data.id }</label>
          </div>
          <div>
            <label>Title: {data.title}</label>
          </div>
          <div>
            <label>Description:  {data.description}</label>
          </div>
          <div>
            <label>DueDate:  {data.dueDate}</label>
          </div>
          <div>
            <label>Status:  {data.status}</label>
          </div>
          <div>
            <label>Priority:  {data.priority}</label>
          </div>
          <div>
            <label>AssignedTo:  {data.assignedTo}</label>
          </div>
        </div>
      </div>
    </div>
  );
};

export default TaskModal;
