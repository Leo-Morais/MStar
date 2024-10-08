import MovimentacaoForm from '../project/MovimentacaoForm'; // Importa o componente de formulário de movimentação
import styles from './NewProjects.module.css'; // Importa o CSS para estilização

function MovimentacaoCadastro() {
    return (
        <div className={styles.newproject_container}>
            <h1>Cadastro de Movimentação</h1>
            <p>Cadastre uma nova movimentação associada a uma mercadoria existente.</p>
            <MovimentacaoForm btnText="Enviar" /> {/* Passa o texto do botão */}
        </div>
    );
}

export default MovimentacaoCadastro;
