
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
  age: number; 
  dateOfBirth: Date;
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
}
export interface RemoveChildDto {
  childId: number;
}
export interface EditChildDto {
  childId: number;
  name: string;
  dateOfBirth: Date; 
  avatarUrl?: string;
}