import styles from './Home.module.css'
import LinkButton from '../layout/LinkButton'

function Home(){
    return (
        <section className={styles.home_container}>
            <h1>Bem-vindo ao <span>Cadastro</span></h1>
            <p>Comece a gerenciar seus projetos agora mesmo!</p>
            
            <div className={styles.button_group}>
                <LinkButton to="/tipoMercadoriaCadastro" text="TipoMercadoria"/>
                <LinkButton to="/mercadoria" text="Mercadoria"/>
                <LinkButton to="/estoque" text="Estoque"/>
                <LinkButton to="/movimentacao" text="Movimentação"/>
            </div>
        </section>
    )
}

export default Home
