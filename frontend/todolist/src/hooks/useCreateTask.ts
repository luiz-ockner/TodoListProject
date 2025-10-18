// src/hooks/useCreateTask.ts

import { useMutation, useQueryClient } from '@tanstack/react-query';
import apiClient from '../api/config';

// Tipo do payload que será enviado (o que a API espera)
interface CreateTaskPayload {
  title: string;
}

// 1. Função que faz a requisição HTTP POST
const createTask = async (payload: CreateTaskPayload) => {
  const response = await apiClient.post('/Tasks', payload);
  return response.data; // Deve retornar o ID da nova tarefa
};

// 2. Custom Hook que usa useMutation
export const useCreateTask = () => {
  const queryClient = useQueryClient(); // Para acessar o cliente e invalidar o cache

  return useMutation({
    mutationFn: createTask, // A função HTTP que será executada
    
    // Onde a mágica acontece: após o sucesso do POST
    onSuccess: () => {
      // Invalida o cache da chave 'tasks'
      // Isso força o useTasks (que usa a queryKey: ['tasks']) a rodar novamente,
      // buscando a lista atualizada do backend automaticamente!
      queryClient.invalidateQueries({ queryKey: ['tasks'] });
    },
    
    // Opcional: para feedback de erro
    onError: (error) => {
      console.error("Erro ao criar tarefa:", error);
      // Você pode adicionar lógica de toast/notificação aqui
    }
  });
};