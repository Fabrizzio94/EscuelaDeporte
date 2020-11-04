--
-- PostgreSQL database dump
--

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: alumno; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.alumno (
    id_alumno character varying NOT NULL,
    nom_alumno character varying,
    sexo character varying,
    fecha_nacimiento date,
    edad integer,
    ciudad character varying,
    provincia character varying,
    nacionalidad character varying,
    direccion_dom character varying,
    tipo_sangre character varying,
    num_uniforme integer,
    id_representante character varying
);


ALTER TABLE public.alumno OWNER TO postgres;

--
-- Name: representante; Type: TABLE; Schema: public; Owner: postgres; Tablespace: 
--

CREATE TABLE public.representante (
    id_representante character varying NOT NULL,
    nombre character varying,
    parentesco character varying,
    celular character varying,
    observaciones character varying
);


ALTER TABLE public.representante OWNER TO postgres;

--
-- Data for Name: alumno; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.alumno (id_alumno, nom_alumno, sexo, fecha_nacimiento, edad, ciudad, provincia, nacionalidad, direccion_dom, tipo_sangre, num_uniforme, id_representante) FROM stdin;
1	fabricio	masculino	1994-12-27	25	machala	el oro	ecuador	santa ines	B+	14	0700963952
0000001	fabricio	Masculino	1990-11-27	29	Cuenca	Azuay	ecu	keti	A+	16	0700963952
\.


--
-- Data for Name: representante; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.representante (id_representante, nombre, parentesco, celular, observaciones) FROM stdin;
0700963952	rosa	madre	123456789	madre
\.


--
-- Name: id_alumno; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.alumno
    ADD CONSTRAINT id_alumno PRIMARY KEY (id_alumno);


--
-- Name: id_representante; Type: CONSTRAINT; Schema: public; Owner: postgres; Tablespace: 
--

ALTER TABLE ONLY public.representante
    ADD CONSTRAINT id_representante PRIMARY KEY (id_representante);


--
-- Name: fki_alumno_representante; Type: INDEX; Schema: public; Owner: postgres; Tablespace: 
--

CREATE INDEX fki_alumno_representante ON public.alumno USING btree (id_representante);


--
-- Name: alumno; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.alumno
    ADD CONSTRAINT alumno FOREIGN KEY (id_representante) REFERENCES public.representante(id_representante);


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM postgres;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

