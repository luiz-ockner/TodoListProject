import { useQuery } from '@tanstack/react-query';
import apiClient from '../api/config';
import type { Task } from '../types/task';

// Função que faz a requisição HTTP (endpoint: GET /api/Tasks)
const fetchTasks = async (): Promise<Task[]> => {
  const response = await apiClient.get<Task[]>('/Tasks');
  return response.data;
};

// Custom Hook que usa o React Query
export const useTasks = () => {
  return useQuery({
    // A 'queryKey' é uma chave única que o React Query usa para cache e invalidação
    queryKey: ['tasks'], 
    queryFn: fetchTasks,
    // Outras opções úteis:
    // staleTime: 1000 * 60, // 1 minuto de dados "frescos"
  });
};