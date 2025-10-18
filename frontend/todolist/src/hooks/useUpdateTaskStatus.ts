import { useMutation, useQueryClient } from '@tanstack/react-query';
import apiClient from '../api/config';

// Tipo do payload para o PATCH
interface UpdateTaskPayload {
    id: number;
    isCompleted: boolean;
}

const updateTaskStatus = async({id,isCompleted}: UpdateTaskPayload) => {
 
    const response = await apiClient.patch(`/tasks/${id}`, {isCompleted},{
        headers: {
            'Content-Type': 'application/json',
        },
    });

    return response.data;    
};

// 2. Custom Hook que usa useMutation
export const useUpdateTaskStatus = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: updateTaskStatus,
        
        onSuccess: () => {
            queryClient.invalidateQueries({queryKey: ['tasks']});
        },

        onError: (error: unknown) => {
            console.error('Erro ao atualizar status:', error);
        }
    });
};

