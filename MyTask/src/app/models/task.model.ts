export enum TaskStatus {
  TODO = 'TODO',
  IN_PROGRESS = 'IN_PROGRESS',
  DONE = 'DONE',
}

export interface Task {
  id: number;
  orderNumber: number;
  title: string;
  description?: string;
  status: TaskStatus;
}
