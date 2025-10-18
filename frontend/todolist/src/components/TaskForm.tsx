// src/components/TaskForm.tsx

import React, { useState } from 'react';
import { useCreateTask } from '../hooks/useCreateTask';

const TaskForm: React.FC = () => {
  const [title, setTitle] = useState('');
  // 1. Chamar o hook de mutação
  const { mutate, isPending, isSuccess } = useCreateTask();

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (title.trim()) {
      // 2. Chama a função mutate com o payload
      mutate({ title });
      setTitle(''); // Limpa o campo
    }
  };
  
  // Exemplo simples de feedback
  const buttonText = isPending ? 'Criando...' : 'Adicionar Tarefa';

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: '20px' }}>
      <input
        type="text"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Título da nova tarefa"
        disabled={isPending}
        style={{ padding: '8px', marginRight: '10px' }}
      />
      <button type="submit" disabled={isPending}>
        {buttonText}
      </button>
      {isSuccess && <span style={{ marginLeft: '10px', color: 'green' }}>✓ Sucesso!</span>}
    </form>
  );
};

export default TaskForm;