import { Component, OnInit } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { AuthService } from '../../services/auth.service';
import { Task, TaskStats, TaskPriority, TaskCategory, TaskPriorityLabels, TaskCategoryLabels } from '../../models/task.model';
import { User } from '../../models/auth.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  tasks: Task[] = [];
  filteredTasks: Task[] = [];
  stats: TaskStats | null = null;
  currentUser: User | null = null;
  loading = false;
  
  // Filter properties
  searchTerm = '';
  selectedCategory: TaskCategory | null = null;
  selectedPriority: TaskPriority | null = null;
  showCompleted = false;
  
  // Enums for template
  TaskPriority = TaskPriority;
  TaskCategory = TaskCategory;
  TaskPriorityLabels = TaskPriorityLabels;
  TaskCategoryLabels = TaskCategoryLabels;

  constructor(
    private taskService: TaskService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });
    
    this.loadTasks();
    this.loadStats();
  }

  loadTasks(): void {
    this.loading = true;
    this.taskService.getTasks().subscribe({
      next: (tasks) => {
        this.tasks = tasks;
        this.applyFilters();
        this.loading = false;
      },
      error: (error) => {
        console.error('Vazifalarni yuklashda xatolik:', error);
        this.loading = false;
      }
    });
  }

  loadStats(): void {
    this.taskService.getTaskStats().subscribe({
      next: (stats) => {
        this.stats = stats;
      },
      error: (error) => {
        console.error('Statistikani yuklashda xatolik:', error);
      }
    });
  }

  applyFilters(): void {
    this.filteredTasks = this.tasks.filter(task => {
      // Search filter
      if (this.searchTerm) {
        const searchLower = this.searchTerm.toLowerCase();
        if (!task.title.toLowerCase().includes(searchLower) && 
            !task.description.toLowerCase().includes(searchLower)) {
          return false;
        }
      }
      
      // Category filter
      if (this.selectedCategory !== null && task.category !== this.selectedCategory) {
        return false;
      }
      
      // Priority filter
      if (this.selectedPriority !== null && task.priority !== this.selectedPriority) {
        return false;
      }
      
      // Completion filter
      if (this.showCompleted && !task.isCompleted) {
        return false;
      }
      
      return true;
    });
  }

  onSearchChange(): void {
    this.applyFilters();
  }

  onCategoryChange(): void {
    this.applyFilters();
  }

  onPriorityChange(): void {
    this.applyFilters();
  }

  onCompletedFilterChange(): void {
    this.applyFilters();
  }

  toggleTaskCompletion(task: Task): void {
    this.taskService.toggleTaskCompletion(task.id).subscribe({
      next: (updatedTask) => {
        const index = this.tasks.findIndex(t => t.id === task.id);
        if (index !== -1) {
          this.tasks[index] = updatedTask;
          this.applyFilters();
          this.loadStats();
        }
      },
      error: (error) => {
        console.error('Vazifa holatini o\'zgartirishda xatolik:', error);
      }
    });
  }

  deleteTask(task: Task): void {
    if (confirm(`"${task.title}" vazifasini o'chirishni xohlaysizmi?`)) {
      this.taskService.deleteTask(task.id).subscribe({
        next: () => {
          this.tasks = this.tasks.filter(t => t.id !== task.id);
          this.applyFilters();
          this.loadStats();
        },
        error: (error) => {
          console.error('Vazifani o\'chirishda xatolik:', error);
        }
      });
    }
  }

  getPriorityClass(priority: TaskPriority): string {
    switch (priority) {
      case TaskPriority.Low: return 'priority-low';
      case TaskPriority.Medium: return 'priority-medium';
      case TaskPriority.High: return 'priority-high';
      case TaskPriority.Critical: return 'priority-critical';
      default: return 'priority-medium';
    }
  }

  getCategoryIcon(category: TaskCategory): string {
    switch (category) {
      case TaskCategory.Work: return '💼';
      case TaskCategory.Personal: return '👤';
      case TaskCategory.Health: return '🏥';
      case TaskCategory.Education: return '📚';
      case TaskCategory.Shopping: return '🛒';
      case TaskCategory.Travel: return '✈️';
      case TaskCategory.Sport: return '⚽';
      default: return '📝';
    }
  }

  isOverdue(task: Task): boolean {
    if (!task.dueDate || task.isCompleted) return false;
    return new Date(task.dueDate) < new Date();
  }

  logout(): void {
    this.authService.logout();
  }
}