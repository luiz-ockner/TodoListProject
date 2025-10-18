import { useTasks } from './hooks/useTasks'
import TaskForm from './components/TaskForm';
import { useUpdateTaskStatus } from './hooks/useUpdateTaskStatus';
import { useDeleteTask } from './hooks/useDeleteTask';

function App() {
  // Chamada do nosso custom hook que gerencia o estado da requisição
  const { data: tasks, isLoading, isError, error } = useTasks();
  const updateMutation = useUpdateTaskStatus(); //Instancia a mutation
  const deleteMutation = useDeleteTask(); // Instancia a mutation de exclusão

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

  const handleDelete = (taskId:number) => {
    deleteMutation.mutate(taskId);
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
            style={{
              display: 'flex',
              alignItems: 'center',
              justifyContent: 'space-between',
              marginBottom: '8px',
              opacity: deleteMutation.isPending && deleteMutation.variables === task.id ? 0.4 : 1, // Feedback visual de exclusão
            }}
          >
            {/* O texto da tarefa é clicável para alternar status */}
            <span
              onClick={() => handleToggleStatus(task.id, task.isCompleted)}
              style={{ 
                cursor: 'pointer',
                flexGrow: 1,
                textDecoration: task.isCompleted ? 'line-through' : 'none',
              }}
            >
               {task.title}
               {task.isCompleted && ' (Concluída)'}
            </span>  

            <button
              onClick={(e) =>{
                e.stopPropagation(); // Impede que o clique no botão dispare o onClick do span pai
                handleDelete(task.id);
              }}
              disabled={deleteMutation.isPending} 
              style={{ marginLeft: '15px', padding: '5px 10px',background: '#e00',color:'white',border: 'none',cursor: 'pointer' }}
              >
                Excluir
              </button>     
          </li>
        ))}
      </ul>

      {updateMutation.isPending && <div>Atualizando...</div>}
      {deleteMutation.isPending && <div>Excluindo tarefa...</div>}
    </div>
  )
}

export default App
