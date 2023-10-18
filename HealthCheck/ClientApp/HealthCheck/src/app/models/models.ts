export interface Result {
  checks: Check[];
  totalStatus: string;
  totalResponseTime: number;
}

export interface Check {
  name: string;
  status: string;
  responseTime: number;
  description: string;
}
