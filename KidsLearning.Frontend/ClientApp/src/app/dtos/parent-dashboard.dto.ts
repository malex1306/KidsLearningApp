// src/app/dtos/parent-dashboard.dto.ts
export interface SubjectProgressDto {
  subjectName: string;
  progressPercentage: number;
}

export interface ChildDto {
  childId: number;
  name: string;
  avatarUrl: string;
  lastActivity: string;
  progress: SubjectProgressDto[];
}

export interface ParentDashboardDto {
  welcomeMessage: string;
  children: ChildDto[];
  recentActivities: string[];
}

export interface AddChildDto {
  name: string;
  avatarUrl?: string;
}