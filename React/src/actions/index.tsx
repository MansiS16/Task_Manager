import { ITask } from "../component/Task.type";

export const Task_Details = "Task_Details";

export const task_data = (item: ITask[]) => {
  return {
    type: Task_Details,
    info: item,
  };
};

export const Task_edit = "Task_edit";

export const edit_data = (item: ITask) => {
  return {
    type: Task_edit,
    info: item,
  };
};
