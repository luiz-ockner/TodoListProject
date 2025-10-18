import { useTasks } from './hooks/useTasks'
import TaskForm from './components/TaskForm';
import { useUpdateTaskStatus } from './hooks/useUpdateTaskStatus';

function App() {
  // Chamada do nosso custom hook que gerencia o estado da requisição
  const { data: tasks, isLoading, isError, error } = useTasks();
  const updateMutation = useUpdateTaskStatus(); //Instancia a mutation

  if (isLoading) {
    return <div>Carregando tarefas...</div>;
  }

  if (isError) {
    // Exibe o erro retornado pelo Axios ou pela API
    console.error(error);
    return <div>Ocorreu um erro ao carregar as tarefas: {(error as Error).message}</div>;
  }

  const handleToggleStatus = (taskId: number, currentStatus: boolean) => {
    // Chama a mutation com o novo status invertido
    updateMutation.mutate({
      id: taskId,
      isCompleted: !currentStatus,
    });
  };

  return (
    <div style={{ padding: '20px' }}>
      <h1>Lista de Tarefas</h1>

      <TaskForm />

      {tasks && tasks.length === 0 && (
        <p>Nenhuma tarefa encontrada. Que tal criar uma?</p>
      )}

      <ul>
        {tasks?.map(task => (
          <li
            key={task.id}
            onClick={() => handleToggleStatus(task.id, task.isCompleted)}
            style={{
              cursor: 'pointer', 
              textDecoration: task.isCompleted ? 'line-through' : 'none', 
              opacity: updateMutation.isPending ? 0.7 : 1,
            }}
          >
            {task.title}
            {task.isCompleted && ' (Concluída)'}
          </li>
        ))}
      </ul>

      {updateMutation.isPending && <div>Atualizando status...</div>}
    </div>
  )
}

export default App
