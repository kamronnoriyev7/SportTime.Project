export interface Task {
  id: number;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt: Date;
  completedAt?: Date;
  dueDate?: Date;
  priority: TaskPriority;
  category: TaskCategory;
  userId: string;
}

export interface TaskCreate {
  title: string;
  description: string;
  dueDate?: Date;
  priority: TaskPriority;
  category: TaskCategory;
}

export interface TaskUpdate {
  title: string;
  description: string;
  isCompleted: boolean;
  dueDate?: Date;
  priority: TaskPriority;
  category: TaskCategory;
}

export interface TaskStats {
  totalTasks: number;
  completedTasks: number;
  pendingTasks: number;
  overdueTasks: number;
  completionRate: number;
}

export enum TaskPriority {
  Low = 1,
  Medium = 2,
  High = 3,
  Critical = 4
}

export enum TaskCategory {
  General = 1,
  Work = 2,
  Personal = 3,
  Health = 4,
  Education = 5,
  Shopping = 6,
  Travel = 7,
  Sport = 8
}

export const TaskPriorityLabels = {
  [TaskPriority.Low]: 'Past',
  [TaskPriority.Medium]: 'O\'rta',
  [TaskPriority.High]: 'Yuqori',
  [TaskPriority.Critical]: 'Kritik'
};

export const TaskCategoryLabels = {
  [TaskCategory.General]: 'Umumiy',
  [TaskCategory.Work]: 'Ish',
  [TaskCategory.Personal]: 'Shaxsiy',
  [TaskCategory.Health]: 'Salomatlik',
  [TaskCategory.Education]: 'Ta\'lim',
  [TaskCategory.Shopping]: 'Xarid',
  [TaskCategory.Travel]: 'Sayohat',
  [TaskCategory.Sport]: 'Sport'
};