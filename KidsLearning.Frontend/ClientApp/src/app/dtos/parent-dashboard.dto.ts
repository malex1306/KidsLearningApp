// src/app/dtos/parent-dashboard.dto.ts

export interface SubjectProgressDto {
  subjectName: string;
  progressPercentage: number;
}

export interface BadgeDto {
  name: string;
  iconUrl: string;
}

export interface AvatarDto {
   id: number;
  name: string;
  imageUrl: string;
  description: string;
  unlockStarRequirement: number;
}

export interface ChildDto {
  childId: number;
  name: string;
  avatarUrl: string;
  lastActivity: string;
  progress: SubjectProgressDto[];
  age: number;
  dateOfBirth: Date;
  starCount: number;
  badges: BadgeDto[];
  unlockedAvatars: AvatarDto[];
  difficulty: string;
}

export interface ParentDashboardDto {
  welcomeMessage: string;
  children: ChildDto[];
  recentActivities: string[];
}

export interface AddChildDto {
  name: string;
  avatarUrl?: string;
  dateOfBirth: Date;
  difficulty: string;
}

export interface RemoveChildDto {
  childId: number;
}

export interface EditChildDto {
  childId: number;
  name: string;
  dateOfBirth: Date;
  avatarUrl?: string;
  difficulty: string;
}
