export class Status {
  id: number = 0;
  name: string = "new";
  isHidden: boolean = false;
  orderNumber: number = 0;

  constructor(id: number, name: string) {
    this.id = id;
    this.name = name;
    this.isHidden = false;
    this.orderNumber = 0;
  }
}

export class ExecutorTask {
  id: number = 0;
  name: string = "new";
  createAt: Date;
  updateAt: Date;

  constructor(id: number, name: string, createAt: string, updateAt: string) {
    this.id = id;
    this.name = name;
    this.createAt = new Date();
    this.updateAt = new Date();
  }
}

export class TypeTask{
   id: number = 0;
   name: string = "new";
   color: string = "#fff";
}

export class MyTask {
    id: number = 0;
    title!: string;
    description?: string;

    deadline?: Date;
    plannedTime?: number;

    isArchive?: boolean;
    statusId!: number;
    status?: Status;
    executorId?: number | null;
    executor?: ExecutorTask | null;
    orderNumber: number = 0;
    totalTime?: number;

    taskTypesId?: number | null;
    taskType?: TypeTask | null;
    isFixed: boolean  = false;

    autorId?: number | null;
    modifiedId?: number | null;

    createAt?: Date;
    updateAt?: Date;
}
