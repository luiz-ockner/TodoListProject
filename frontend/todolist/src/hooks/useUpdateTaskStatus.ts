// src/hooks/useUpdateTaskStatus.ts

import { useMutation, useQueryClient } from '@tanstack/react-query';
import apiClient from '../api/config';

// Tipo do payload para o PATCH
interface UpdateTaskPayload {
  id: number;
  isCompleted: boolean;
}

// 1. Função que faz a requisição HTTP PATCH
const updateTaskStatus = async ({ id, isCompleted }: UpdateTaskPayload) => {
  // PATCH /api/Tasks/{id}/status
  const response = await apiClient.patch(`/Tasks/${id}/status`, isCompleted, {
    // Definimos explicitamente o Content-Type como application/json, 
    // embora o isCompleted seja um booleano, a API espera no corpo da requisição.
    headers: { 'Content-Type': 'application/json' }
  });
  return response.data;
};

// 2. Custom Hook que usa useMutation
export const useUpdateTaskStatus = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateTaskStatus,
    
    // ONDE A MÁGICA ACONTECE: Força o refetch da lista após o sucesso.
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['tasks'] });
    },
    
    onError: (error) => {
      console.error("Erro ao atualizar status:", error);
    }
  });
};