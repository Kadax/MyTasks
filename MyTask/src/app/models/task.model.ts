export class Status {
  id: number = 0;
  name: string = "new";

  constructor(id: number, name: string) {
    this.id = id;
    this.name = name;
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

export class MyTask {
    id!: number;
    title!: string;
    description?: string;
    isArchive?: boolean;
    statusId!: number;
    status?: Status;
    executorId?: number | null;
    executor?: null;
    orderNumber: number = 0;
    totalTime?: number;
    createAt?: Date;
    updateAt?: Date;
}
