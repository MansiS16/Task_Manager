export interface ITask {
      id: number;
      title: string;
      description: string;
      dueDate: string;
      status:string;
      priority:string;
      assignedTo:string;
      assignedBy:string | null;
}

export enum PageEnum {
  list,
  add,
  edit,
}
